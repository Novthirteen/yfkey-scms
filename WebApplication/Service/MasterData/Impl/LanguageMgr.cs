using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Castle.Services.Transaction;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Utility;
using System.Linq;

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class LanguageMgr : SessionBase, ILanguageMgr
    {
        private string languageFileFolder;
        private IDictionary<string, IDictionary<string, string>> languageDic;
        private ICodeMasterMgr codeMasterMgr;
        private IEntityPreferenceMgr entityPreferenceMgr;
        private IUserMgr userMgr;

        private Regex regex = new Regex("\\${[\\w \\. , -]+?}", RegexOptions.Singleline);
        private char[] prefix = new char[] { '$', '{' };
        private char[] surfix = new char[] { '}' };

        public LanguageMgr(ICodeMasterMgr codeMasterMgr,
            string languageFileFolder,
            IEntityPreferenceMgr entityPreferenceMgr,
            IUserMgr userMgr)
        {
            this.languageFileFolder = languageFileFolder;
            this.codeMasterMgr = codeMasterMgr;
            this.LoadLanguage();
            this.entityPreferenceMgr = entityPreferenceMgr;
            this.userMgr = userMgr;
        }

        #region ILanguageMgr Members
        public string ProcessLanguage(string content, string language)
        {
            if (languageDic.ContainsKey(language))
            {
                IDictionary<string, string> targetLanguageDic = languageDic[language];
                MatchCollection mc = regex.Matches(content);

                var r = from Match m in mc
                           where mc != null
                           where mc.Count > 0
                           group m by m.Value into result
                           select new
                           {
                               Value = result.Key
                           };

                foreach (var p in r)
                {
                    string[] splitKey = p.Value.TrimStart(prefix).TrimEnd(surfix).Split(',');
                    string actualKey = splitKey[0];
                    if (targetLanguageDic.ContainsKey(actualKey))
                    {
                        //处理Message中的参数
                        string value = targetLanguageDic[actualKey];
                        if (splitKey.Length > 1)
                        {
                            string[] para = new string[splitKey.Length - 1];
                            for (int j = 1; j < splitKey.Length; j++)
                            {
                                para[j - 1] = splitKey[j].Trim();
                            }
                            try
                            {
                                value = string.Format(value, para);
                            }
                            catch (Exception ex)
                            {
                                //throw;
                            }
                        }

                        content = content.Replace(p.Value, value);
                    }
                    
                }
            }
            else
            {
                throw new TechnicalException("没有指定类型的语言：" + language);
            }

            return content;
        }

        public void ReLoadLanguage()
        {
            this.LoadLanguage();
        }
        public string TranslateMessage(string content, string userCode)
        {
            return this.TranslateMessage(content, userCode, null);
        }
        public string TranslateMessage(string content, User user)
        {
            return this.TranslateMessage(content, user, null);
        }
        public string TranslateMessage(string content, string userCode, params string[] parameters)
        {
            User user = userMgr.LoadUser(userCode, true, false);
            return this.TranslateMessage(content, user, parameters);
        }
        public string TranslateMessage(string content, User user, params string[] parameters)
        {
            try
            {
                content = ProcessMessage(content, parameters);

                if (user != null && user.UserLanguage != null && user.UserLanguage != string.Empty)
                {
                    content = this.ProcessLanguage(content, user.UserLanguage);
                }
                else
                {
                    EntityPreference defaultLanguage = entityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_DEFAULT_LANGUAGE);
                    content = this.ProcessLanguage(content, defaultLanguage.Value);
                }
            }
            catch (Exception ex)
            {
                throw new TechnicalException("翻译时出现异常:" + ex.Message);
            }
            return content;
        }
        #endregion

        private void LoadLanguage()
        {

            languageDic = new Dictionary<string, IDictionary<string, string>>();
            IList<CodeMaster> languages = codeMasterMgr.GetCachedCodeMaster(BusinessConstants.CODE_MASTER_LANGUAGE);
            foreach (CodeMaster language in languages)
            {
                string languageKey = language.Value;
                string resourceFile = languageFileFolder + "/ApplicationResources_" + languageKey + ".properties";
                IDictionary<string, string> targetLanguageDic = new Dictionary<string, string>();

                PropertyFileReader propertyFileReader = new PropertyFileReader(resourceFile);
                while (!propertyFileReader.EndOfStream)
                {
                    string[] property = propertyFileReader.GetPropertyLine();
                    if (property != null)
                    {
                        try
                        {
                            targetLanguageDic.Add(property[0].Trim(), property[1].Trim());
                        }
                        catch (Exception)
                        {
                        }
                    }
                }

                languageDic.Add(languageKey, targetLanguageDic);
            }
        }

        private string ProcessMessage(string message, string[] paramters)
        {
            string messageParams = string.Empty;
            if (paramters != null && paramters.Length > 0)
            {
                //处理Message参数
                foreach (string para in paramters)
                {
                    messageParams += "," + para;
                }
            }
            message = "${" + message + messageParams + "}";

            return message;
        }
    }
}
