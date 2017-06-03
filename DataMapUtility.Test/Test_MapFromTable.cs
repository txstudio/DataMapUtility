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
        public void Read_To_Min_Object_Property()
        {
            var _table = this._userManager.GetUserTable();
            var _users = DataMapUtility.MapFromTable<UserMin>(_table);

            foreach (var _user in _users)
            {
                Assert.AreNotEqual<int>(_user.No, 0);
                Assert.AreNotEqual<string>(_user.LastName, string.Empty);
            }
        }

        [TestMethod]
        public void Read_To_More_Object_Property()
        {
            var _table = this._userManager.GetUserTable();
            var _users = DataMapUtility.MapFromTable<UserMore>(_table);
            var _isEmpty = false;

            foreach (var _user in _users)
            {
                _isEmpty = string.IsNullOrWhiteSpace(_user.Company);

                Assert.AreEqual<bool>(_isEmpty, true);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Read_To_Error_Property()
        {
            var _table = this._userManager.GetUserTable();
            var _users = DataMapUtility.MapFromTable<UserWithErrorDataType>(_table);
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
