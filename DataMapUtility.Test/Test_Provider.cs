using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapUtility.Test
{

    public abstract class TestProvider
    {
        protected readonly UserManager _userManager;

        public TestProvider()
        {
            this._userManager = new UserManager();
        }
    }

}
