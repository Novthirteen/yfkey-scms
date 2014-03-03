
using com.Sconit.Entity.MasterData;
namespace com.Sconit.Service.MasterData
{
    public interface ILanguageMgr
    {
        string ProcessLanguage(string content, string language);

        void ReLoadLanguage();

        string TranslateMessage(string content, string userCode);

        string TranslateMessage(string content, User user);

        string TranslateMessage(string content, string userCode, params string[] parameters);

        string TranslateMessage(string content, User user, params string[] parameters);
    }
}
