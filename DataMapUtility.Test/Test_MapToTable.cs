using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DataMapUtility.Test
{
    [TestClass]
    public sealed class MapToTable : TestProvider
    {

        [TestMethod]
        public void Read_From_Standard_Collection()
        {
            string _expected;
            string _actual;

            IEnumerable<User> _users;
            DataTable _table;

            _users = this._userManager.GetUsers();
            _table = DataMapUtility.MapToTable<User>(_users);


            _expected = JsonSerializer.ToJson(_users);
            _actual = JsonSerializer.ToJson(_table);
            

            Assert.AreEqual(_expected, _actual);   
        }

        [TestMethod]
        public void Read_From_Empty_Collection()
        {
            string _expected;
            string _actual;

            IEnumerable<User> _users;
            IEnumerable<string> _columns;
            List<string> _dataColumns;
            DataTable _table;

            _users = null;
            _table = DataMapUtility.MapToTable<User>(_users);
            _columns = new string[] { "No", "LastName", "MiddleName", "FirstName", "NickName", "Birthday" };

            _dataColumns = new List<string>();
            
            foreach (DataColumn column in _table.Columns)
                _dataColumns.Add(column.ColumnName);


            _expected = JsonSerializer.ToJson(_columns);
            _actual = JsonSerializer.ToJson(_dataColumns);


            Assert.AreEqual(_expected, _actual);
        }

    }
}
