using NUnit.Framework;
using WatiN.Core;
using WatiN.Core.Native.Windows;

namespace SconitTesting.Utility
{
    public static class Utilities
    {
        public static string GetUrl(string url)
        {
            return ConfigurationReader.getHomePageUrl() + url;
        }

        public static void NavigateToHomePage(ref IE ieBrowser)
        {
            if (ieBrowser == null)
            {
                ieBrowser = new IE();
                ieBrowser.ShowWindow(NativeMethods.WindowShowStyle.Maximize);
            }

            ieBrowser.GoTo(ConfigurationReader.getHomePageUrl() + "Default.aspx");
            ieBrowser.WaitForComplete();
        }

        public static object FindControlInBrowserByID(IE ie, string strID, Enumerators.ControlType ctrl)
        {
            if (ctrl == Enumerators.ControlType.Span)
            {
                Span sp = ie.Span(Find.ById(strID));
                Assert.IsTrue(sp.Exists, "Could not Find: " + strID);
                return sp;
            }
            else if (ctrl == Enumerators.ControlType.Link)
            {
                Link lnk = ie.Link(Find.ById(strID));
                Assert.IsTrue(lnk.Exists, "Could not Find: " + strID);
                return lnk;
            }
            else if (ctrl == Enumerators.ControlType.Frame)
            {
                Frame iFrame = ie.Frame(Find.ById(strID));
                return iFrame;
            }
            else if (ctrl == Enumerators.ControlType.Image)
            {
                Image img = ie.Image(Find.ById(strID));
                Assert.IsTrue(img.Exists, "Could not Find: " + strID);
                return img;
            }
            else if (ctrl == Enumerators.ControlType.TableCell)
            {
                TableCell tCell = ie.TableCell(Find.ById(strID));
                Assert.IsTrue(tCell.Exists, "Could not Find: " + strID);
                return tCell;
            }
            else if (ctrl == Enumerators.ControlType.Table)
            {
                Table tbl = ie.Table(Find.ById(strID));
                Assert.IsTrue(tbl.Exists, "Could not Find: " + strID);
                return tbl;
            }
            else if (ctrl == Enumerators.ControlType.TableRow)
            {
                TableRow row = ie.TableRow(Find.ById(strID));
                Assert.IsTrue(row.Exists, "Could not Find: " + strID);
                return row;
            }
            else if (ctrl == Enumerators.ControlType.CheckBox)
            {
                CheckBox chk = ie.CheckBox(Find.ById(strID));
                Assert.IsTrue(chk.Exists, "Could not Find: " + strID);
                return chk;
            }
            else if (ctrl == Enumerators.ControlType.Button)
            {
                Button btn = ie.Button(Find.ById(strID));
                Assert.IsTrue(btn.Exists, "Could not Find: " + strID);
                return btn;
            }
            else if (ctrl == Enumerators.ControlType.TextField)
            {
                TextField txt = ie.TextField(Find.ById(strID));
                Assert.IsTrue(txt.Exists, "Could not Find: " + strID);
                return txt;
            }
            else if (ctrl == Enumerators.ControlType.SelectList)
            {
                SelectList sList = ie.SelectList(Find.ById(strID));
                Assert.IsTrue(sList.Exists, "Could not Find: " + strID);
                return sList;
            }
            else if (ctrl == Enumerators.ControlType.Div)
            {
                Div division = ie.Div(Find.ById(strID));
                Assert.IsTrue(division.Exists, "Could not Find: " + strID);
                return division;
            }
            else if (ctrl == Enumerators.ControlType.TableRow)
            {
                TableRow tRow = ie.TableRow(Find.ById(strID));
                Assert.IsTrue(tRow.Exists, "Could not Find: " + strID);
                return tRow;
            }
            else if (ctrl == Enumerators.ControlType.FileUpload)
            {
                FileUpload fileUpload = ie.FileUpload(Find.ById(strID));
                Assert.IsTrue(fileUpload.Exists, "Could not find: " + strID);
                return fileUpload;
            }
            else
            {
                return null;
            }
        }

        public static object FindControlInBrowserByCustom(IE ie, string strCustomAttribute, string strToFind,
                                                          Enumerators.ControlType ctrl)
        {
            if (ctrl == Enumerators.ControlType.Span)
            {
                Span sp = ie.Span(Find.By(strCustomAttribute, strToFind));
                Assert.IsTrue(sp.Exists, "Could not Find: " + strToFind);
                return sp;
            }
            else if (ctrl == Enumerators.ControlType.Link)
            {
                Link lnk = ie.Link(Find.By(strCustomAttribute, strToFind));
                Assert.IsTrue(lnk.Exists, "Could not Find: " + strToFind);
                return lnk;
            }
            else if (ctrl == Enumerators.ControlType.Frame)
            {
                Frame frame = ie.Frame(Find.By(strCustomAttribute, strToFind));
                Assert.AreEqual(frame.Name, strToFind);
                return frame;
            }
            else if (ctrl == Enumerators.ControlType.Image)
            {
                Image img;

                if (strCustomAttribute == "src")
                {
                    img = ie.Image(Find.BySrc(strToFind));
                }
                else
                {
                    img = ie.Image(Find.By(strCustomAttribute, strToFind));
                }
                Assert.IsTrue(img.Exists, "Could not Find: " + strToFind);
                return img;
            }
            else if (ctrl == Enumerators.ControlType.TableCell)
            {
                TableCell tCell = ie.TableCell(Find.By(strCustomAttribute, strToFind));
                Assert.IsTrue(tCell.Exists, "Could not Find: " + strToFind);
                return tCell;
            }
            else if (ctrl == Enumerators.ControlType.Table)
            {
                Table tbl = ie.Table(Find.By(strCustomAttribute, strToFind));
                Assert.IsTrue(tbl.Exists, "Could not Find: " + strToFind);
                return tbl;
            }
            else if (ctrl == Enumerators.ControlType.TableRow)
            {
                TableRow row = ie.TableRow(Find.By(strCustomAttribute, strToFind));
                Assert.IsTrue(row.Exists, "Could not Find: " + strToFind);
                return row;
            }
            else if (ctrl == Enumerators.ControlType.CheckBox)
            {
                CheckBox chk = ie.CheckBox(Find.By(strCustomAttribute, strToFind));
                Assert.IsTrue(chk.Exists, "Could not Find: " + strToFind);
                return chk;
            }
            else if (ctrl == Enumerators.ControlType.Button)
            {
                Button btn = ie.Button(Find.By(strCustomAttribute, strToFind));
                Assert.IsTrue(btn.Exists, "Could not Find: " + strToFind);
                return btn;
            }
            else if (ctrl == Enumerators.ControlType.TextField)
            {
                TextField txt = ie.TextField(Find.By(strCustomAttribute, strToFind));
                Assert.IsTrue(txt.Exists, "Could not Find: " + strToFind);
                return txt;
            }
            else if (ctrl == Enumerators.ControlType.SelectList)
            {
                SelectList sList = ie.SelectList(Find.By(strCustomAttribute, strToFind));
                Assert.IsTrue(sList.Exists, "Could not Find: " + strToFind);
                return sList;
            }
            else if (ctrl == Enumerators.ControlType.Div)
            {
                Div div = ie.Div(Find.By(strCustomAttribute, strToFind));
                Assert.IsTrue(div.Exists, "Could not Find: " + strToFind);
                return div;
            }
            else
            {
                return null;
            }
        }

        public static void SelectControlInBrowserByID(IE ie, string strID, Enumerators.ControlType ctrl)
        {
            if (ctrl == Enumerators.ControlType.Image)
            {
                Image imgCtrl = (Image)FindControlInBrowserByID(ie, strID, Enumerators.ControlType.Image);
                imgCtrl.Click();
            }
            else if (ctrl == Enumerators.ControlType.TableCell)
            {
                TableCell tblCell = (TableCell)FindControlInBrowserByID(ie, strID, Enumerators.ControlType.TableCell);
                tblCell.Click();
            }
            else if (ctrl == Enumerators.ControlType.Button)
            {
                Button btnToSelect = (Button)FindControlInBrowserByID(ie, strID, Enumerators.ControlType.Button);
                btnToSelect.Click();
            }
            else if (ctrl == Enumerators.ControlType.Link)
            {
                Link lnkSelect = (Link)FindControlInBrowserByID(ie, strID, Enumerators.ControlType.Link);
                lnkSelect.Click();
            }
            else if (ctrl == Enumerators.ControlType.CheckBox)
            {
                CheckBox chkToSelect = (CheckBox)FindControlInBrowserByID(ie, strID, Enumerators.ControlType.CheckBox);
                chkToSelect.Click();
            }
            else if (ctrl == Enumerators.ControlType.Div)
            {
                Div divToSelect = (Div)FindControlInBrowserByID(ie, strID, Enumerators.ControlType.Div);
                divToSelect.Click();
            }
            else if (ctrl == Enumerators.ControlType.Span)
            {
                Span spanToSelect = (Span)FindControlInBrowserByID(ie, strID, Enumerators.ControlType.Span);
                spanToSelect.Click();
            }
            ie.WaitForComplete();
        }

        public static void SelectControlInBrowserByCustom(IE ie, string strCustomAttribute, string strToFind,
                                                          Enumerators.ControlType ctrl)
        {
            if (ctrl == Enumerators.ControlType.Image)
            {
                Image imgCtrl =
                    (Image)FindControlInBrowserByCustom(ie, strCustomAttribute, strToFind, Enumerators.ControlType.Image);
                imgCtrl.Click();
            }
            else if (ctrl == Enumerators.ControlType.TableCell)
            {
                TableCell tblCell =
                    (TableCell)FindControlInBrowserByCustom(ie, strCustomAttribute, strToFind, Enumerators.ControlType.TableCell);
                tblCell.Click();
            }
            else if (ctrl == Enumerators.ControlType.Link)
            {
                Link lnkToSelect =
                    (Link)FindControlInBrowserByCustom(ie, strCustomAttribute, strToFind, Enumerators.ControlType.Link);
                lnkToSelect.Click();
            }
            else if (ctrl == Enumerators.ControlType.CheckBox)
            {
                CheckBox chkToClick =
                    (CheckBox)FindControlInBrowserByCustom(ie, strCustomAttribute, strToFind, Enumerators.ControlType.CheckBox);
                chkToClick.Click();
            }
            else if (ctrl == Enumerators.ControlType.Div)
            {
                Div divToClick = (Div)FindControlInBrowserByCustom(ie, strCustomAttribute, strToFind, Enumerators.ControlType.Div);
                divToClick.Click();
            }
            ie.WaitForComplete();
        }

        public static void ClickLink(ref IE ie, string strToFind)
        {
            Link lnk = (Link)FindControlInBrowserByID(ie, strToFind, Enumerators.ControlType.Link);
            lnk.Click();
            ie.WaitForComplete();
        }

        public static void ClickLinkByName(ref IE ie, string strNameToFind)
        {
            SelectControlInBrowserByCustom(ie, "innertext", strNameToFind, Enumerators.ControlType.Link);
        }

        public static void AddTextInTextBox(ref IE ie, string strToFind, string InputValue)
        {
            TextField txtTextField = (TextField)FindControlInBrowserByID(ie, strToFind, Enumerators.ControlType.TextField);
            txtTextField.TypeText(InputValue);
        }

        public static object FindControlInTableByID(Table tbl, string strID, Enumerators.ControlType ctrl)
        {
            if (ctrl == Enumerators.ControlType.Span)
            {
                Span sp = tbl.Span(Find.ById(strID));
                Assert.IsTrue(sp.Exists, "Could not Find: " + strID);
                return sp;
            }
            else if (ctrl == Enumerators.ControlType.Link)
            {
                Link lnk = tbl.Link(Find.ById(strID));
                Assert.IsTrue(lnk.Exists, "Could not Find: " + strID);
                return lnk;
            }
            else if (ctrl == Enumerators.ControlType.Image)
            {
                Image img = tbl.Image(Find.BySrc(strID));
                Assert.IsTrue(img.Exists, "Could not Find: " + strID);
                return img;
            }
            else if (ctrl == Enumerators.ControlType.TableCell)
            {
                TableCell tCell = tbl.TableCell(Find.ById(strID));
                Assert.IsTrue(tCell.Exists, "Could not Find: " + strID);
                return tCell;
            }
            else if (ctrl == Enumerators.ControlType.Table)
            {
                Table nestedTbl = tbl.Table(Find.ById(strID));
                Assert.IsTrue(nestedTbl.Exists, "Could not Find: " + strID);
                return nestedTbl;
            }
            else if (ctrl == Enumerators.ControlType.CheckBox)
            {
                CheckBox chk = tbl.CheckBox(Find.ById(strID));
                Assert.IsTrue(chk.Exists, "Could not Find: " + strID);
                return chk;
            }
            else if (ctrl == Enumerators.ControlType.Button)
            {
                Button btn = tbl.Button(Find.ById(strID));
                Assert.IsTrue(btn.Exists, "Could not Find: " + strID);
                return btn;
            }
            else if (ctrl == Enumerators.ControlType.TextField)
            {
                TextField txt = tbl.TextField(Find.ById(strID));
                Assert.IsTrue(txt.Exists, "Could not Find: " + strID);
                return txt;
            }
            else if (ctrl == Enumerators.ControlType.Div)
            {
                Div division = tbl.Div(Find.ById(strID));
                Assert.IsTrue(division.Exists, "Could not Find: " + strID);
                return division;
            }
            else if (ctrl == Enumerators.ControlType.TableRow)
            {
                TableRow tRow = tbl.TableRow(Find.ById(strID));
                Assert.IsTrue(tRow.Exists, "Could not Find: " + strID);
                return tRow;
            }
            else
            {
                return null;
            }
        }

        public static object FindControlInTableByCustom(Table tbl, string strCustomAttribute, string strToFind,
                                                          Enumerators.ControlType ctrl)
        {
            if (ctrl == Enumerators.ControlType.Span)
            {
                Span sp = tbl.Span(Find.By(strCustomAttribute, strToFind));
                Assert.IsTrue(sp.Exists, "Could not Find: " + strToFind);
                return sp;
            }
            else if (ctrl == Enumerators.ControlType.Link)
            {
                Link lnk = tbl.Link(Find.By(strCustomAttribute, strToFind));
                Assert.IsTrue(lnk.Exists, "Could not Find: " + strToFind);
                return lnk;
            }
            else if (ctrl == Enumerators.ControlType.Image)
            {
                Image img;

                if (strCustomAttribute == "src")
                {
                    img = tbl.Image(Find.BySrc(strToFind));
                }
                else
                {
                    img = tbl.Image(Find.By(strCustomAttribute, strToFind));
                }
                Assert.IsTrue(img.Exists, "Could not Find: " + strToFind);
                return img;
            }
            else if (ctrl == Enumerators.ControlType.TableCell)
            {
                TableCell tCell = tbl.TableCell(Find.By(strCustomAttribute, strToFind));
                Assert.IsTrue(tCell.Exists, "Could not Find: " + strToFind);
                return tCell;
            }
            else if (ctrl == Enumerators.ControlType.Table)
            {
                Table nestedTbl = tbl.Table(Find.By(strCustomAttribute, strToFind));
                Assert.IsTrue(nestedTbl.Exists, "Could not Find: " + strToFind);
                return nestedTbl;
            }
            else if (ctrl == Enumerators.ControlType.TableRow)
            {
                TableRow row = tbl.TableRow(Find.By(strCustomAttribute, strToFind));
                Assert.IsTrue(row.Exists, "Could not Find: " + strToFind);
                return row;
            }
            else if (ctrl == Enumerators.ControlType.CheckBox)
            {
                CheckBox chk = tbl.CheckBox(Find.By(strCustomAttribute, strToFind));
                Assert.IsTrue(chk.Exists, "Could not Find: " + strToFind);
                return chk;
            }
            else if (ctrl == Enumerators.ControlType.Button)
            {
                Button btn = tbl.Button(Find.By(strCustomAttribute, strToFind));
                Assert.IsTrue(btn.Exists, "Could not Find: " + strToFind);
                return btn;
            }
            else if (ctrl == Enumerators.ControlType.TextField)
            {
                TextField txt = tbl.TextField(Find.By(strCustomAttribute, strToFind));
                Assert.IsTrue(txt.Exists, "Could not Find: " + strToFind);
                return txt;
            }
            else if (ctrl == Enumerators.ControlType.SelectList)
            {
                SelectList sList = tbl.SelectList(Find.By(strCustomAttribute, strToFind));
                Assert.IsTrue(sList.Exists, "Could not Find: " + strToFind);
                return sList;
            }
            else if (ctrl == Enumerators.ControlType.Div)
            {
                Div div = tbl.Div(Find.By(strCustomAttribute, strToFind));
                Assert.IsTrue(div.Exists, "Could not Find: " + strToFind);
                return div;
            }
            else
            {
                return null;
            }
        }

        public static object FindControlInFrameByID(Frame frame, string strID, Enumerators.ControlType ctrl)
        {
            if (ctrl == Enumerators.ControlType.Span)
            {
                Span sp = frame.Span(Find.ById(strID));
                Assert.IsTrue(sp.Exists, "Could not Find: " + strID);
                return sp;
            }
            else if (ctrl == Enumerators.ControlType.Link)
            {
                Link lnk = frame.Link(Find.ById(strID));
                Assert.IsTrue(lnk.Exists, "Could not Find: " + strID);
                return lnk;
            }
            else if (ctrl == Enumerators.ControlType.Image)
            {
                Image img = frame.Image(Find.BySrc(strID));
                Assert.IsTrue(img.Exists, "Could not Find: " + strID);
                return img;
            }
            else if (ctrl == Enumerators.ControlType.TableCell)
            {
                TableCell tCell = frame.TableCell(Find.ById(strID));
                Assert.IsTrue(tCell.Exists, "Could not Find: " + strID);
                return tCell;
            }
            else if (ctrl == Enumerators.ControlType.Table)
            {
                Table nestedTbl = frame.Table(Find.ById(strID));
                Assert.IsTrue(nestedTbl.Exists, "Could not Find: " + strID);
                return nestedTbl;
            }
            else if (ctrl == Enumerators.ControlType.CheckBox)
            {
                CheckBox chk = frame.CheckBox(Find.ById(strID));
                Assert.IsTrue(chk.Exists, "Could not Find: " + strID);
                return chk;
            }
            else if (ctrl == Enumerators.ControlType.Button)
            {
                Button btn = frame.Button(Find.ById(strID));
                Assert.IsTrue(btn.Exists, "Could not Find: " + strID);
                return btn;
            }
            else if (ctrl == Enumerators.ControlType.TextField)
            {
                TextField txt = frame.TextField(Find.ById(strID));
                Assert.IsTrue(txt.Exists, "Could not Find: " + strID);
                return txt;
            }
            else if (ctrl == Enumerators.ControlType.Div)
            {
                Div division = frame.Div(Find.ById(strID));
                Assert.IsTrue(division.Exists, "Could not Find: " + strID);
                return division;
            }
            else if (ctrl == Enumerators.ControlType.TableRow)
            {
                TableRow tRow = frame.TableRow(Find.ById(strID));
                Assert.IsTrue(tRow.Exists, "Could not Find: " + strID);
                return tRow;
            }
            else
            {
                return null;
            }
        }
    }
}
