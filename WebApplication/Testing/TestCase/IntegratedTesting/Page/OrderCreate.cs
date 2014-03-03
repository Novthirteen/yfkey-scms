using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;

namespace SconitTesting.TestCase.IntegratedTesting.Page
{
    [Page()]
    public class OrderCreate : PageBase
    {
        [FindBy(Id = "ctl01_ucSearch_btnNew")]
        public Button btnNew;

        [FindBy(Id = "ctl01_ucNew_tbFlow_suggest")]
        public TextField tbFlow;

        [FindBy(Id = "ctl01_ucNew_tbWinTime")]
        public TextField tbWinTime;

        [FindBy(Id = "ctl01_ucNew_btnConfirm")]
        public Button btnConfirm;

        [FindBy(Id = "ctl01_ucNew_ucList_GV_List_ctl02_tbOrderQty")]
        public TextField tbOrderQty1;

        [FindBy(Id = "ctl01_ucNew_ucList_GV_List_ctl03_tbOrderQty")]
        public TextField tbOrderQty2;
    }
}
