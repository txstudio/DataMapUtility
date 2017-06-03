using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataMapUtility.Test
{
    [TestClass]
    public sealed class MapFromTable : TestProvider
    {
        [TestMethod]
        public void Read_From_Standard_Table()
        {
            string _excepted;
            string _actual;

            var _table = this._userManager.GetUserTable();
            var _users = DataMapUtility.MapFromTable<User>(_table);


            _excepted = JsonSerializer.ToJson(_table);
            _actual = JsonSerializer.ToJson(_users);

            Assert.AreEqual<string>(_excepted, _actual);
        }

        [TestMethod]
        public void Read_From_Empty_Table()
        {
            string _excepted;
            string _actual;

            var _table = this._userManager.GetUserTable();

            _table.Rows.Clear();

            var _users = DataMapUtility.MapFromTable<User>(_table);


            _excepted = JsonSerializer.ToJson(_table);
            _actual = JsonSerializer.ToJson(_users);
            
            Assert.AreEqual<string>(_excepted, _actual);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Read_From_Null_Table()
        {
            System.Data.DataTable _table = null;

            var _users = DataMapUtility.MapFromTable<User>(_table);
        }
    }
}
