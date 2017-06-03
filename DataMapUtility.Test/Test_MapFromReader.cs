using System;
using Rhino.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Collections.Generic;

namespace DataMapUtility.Test
{
    [TestClass]
    public class MapFromReader : TestProvider
    { 
        [TestMethod]
        public void Read_From_Standard_Reader()
        {
            MockRepository _mock = new MockRepository();

            var _reader = _mock.StrictMock<IDataReader>();

            Expect.Call(_reader.Read()).Return(true).Repeat.Times(3);
            Expect.Call(_reader.Read()).Return(false);
            Expect.Call(_reader.FieldCount).Return(6).Repeat.Times(7);

            Expect.Call(_reader.GetName(0)).Return("No");
            Expect.Call(_reader.GetName(1)).Return("LastName");
            Expect.Call(_reader.GetName(2)).Return("MiddleName");
            Expect.Call(_reader.GetName(3)).Return("FirstName");
            Expect.Call(_reader.GetName(4)).Return("NickName");
            Expect.Call(_reader.GetName(5)).Return("Birthday");

            //方法裡 IDataReader[ColumnName] 會讀取兩次：要設定 Repeat 為 2
            Expect.Call(_reader["No"]).Return(1).Repeat.Times(2);
            Expect.Call(_reader["FirstName"]).Return("Chien Ming").Repeat.Times(2);
            Expect.Call(_reader["MiddleName"]).Return(DBNull.Value).Repeat.Times(2);
            Expect.Call(_reader["LastName"]).Return("Wang").Repeat.Times(2);
            Expect.Call(_reader["NickName"]).Return(DBNull.Value).Repeat.Times(2);
            Expect.Call(_reader["Birthday"]).Return(new DateTime(1980, 3, 31)).Repeat.Times(2);

            Expect.Call(_reader["No"]).Return(2).Repeat.Times(2);
            Expect.Call(_reader["FirstName"]).Return("Wei Yin").Repeat.Times(2);
            Expect.Call(_reader["MiddleName"]).Return(DBNull.Value).Repeat.Times(2);
            Expect.Call(_reader["LastName"]).Return("Chen").Repeat.Times(2);
            Expect.Call(_reader["NickName"]).Return(DBNull.Value).Repeat.Times(2);
            Expect.Call(_reader["Birthday"]).Return(new DateTime(1985, 7, 21)).Repeat.Times(2);

            Expect.Call(_reader["No"]).Return(3).Repeat.Times(2);
            Expect.Call(_reader["FirstName"]).Return("Dai Kang").Repeat.Times(2);
            Expect.Call(_reader["MiddleName"]).Return(DBNull.Value).Repeat.Times(2);
            Expect.Call(_reader["LastName"]).Return("Yang").Repeat.Times(2);
            Expect.Call(_reader["NickName"]).Return(DBNull.Value).Repeat.Times(2);
            Expect.Call(_reader["Birthday"]).Return(new DateTime(1987, 1, 17)).Repeat.Times(2);


            _mock.ReplayAll();


            string _expected;
            string _actual;
            
            var _objects = this._userManager.GetUsers();
            var _users = DataMapUtility.MapFromReader<User>(_reader);


            _expected = JsonSerializer.ToJson(_objects);
            _actual = JsonSerializer.ToJson(_users);

            Assert.AreEqual(_expected, _actual);
        }

        [TestMethod]
        public void Read_From_Empty_Reader()
        {
            MockRepository _mock = new MockRepository();

            var _reader = _mock.StrictMock<IDataReader>();

            Expect.Call(_reader.Read()).Return(false);

            string _expected;
            string _actual;

            var _emptyUser = new List<User>();
            var _users = DataMapUtility.MapFromReader<User>(_reader);

            _expected = JsonSerializer.ToJson(_emptyUser);
            _actual = JsonSerializer.ToJson(_users);

            Assert.AreEqual(_expected, _actual);
        }
    }
}
