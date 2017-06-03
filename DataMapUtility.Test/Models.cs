using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapUtility.Test
{

    public sealed class User
    {
        public int No { get; set; }

        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FirstName { get; set; }

        public string NickName { get; set; }

        public Nullable<DateTime> Birthday { get; set; }
    }

    public sealed class UserMin
    {
        public int No { get; set; }
        public string LastName { get; set; }
    }

    public sealed class UserMore
    {
        public int No { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public Nullable<DateTime> Birthday { get; set; }
        public string Company { get; set; }
    }

    public sealed class UserWithErrorDataType
    {
        public int No { get; set; }
        public string LastName { get; set; }
        public string Birthday { get; set; }
    }



    public sealed class UserManager
    {
        private List<User> _users;

        public UserManager()
        {
            this._users = new List<User>();
            this._users.Add(new User() {
                No = 1,
                FirstName = "Chien Ming",
                LastName = "Wang",
                Birthday = new DateTime(1980, 3, 31)
            });
            this._users.Add(new User()
            {
                No = 2,
                FirstName = "Wei Yin",
                LastName = "Chen",
                Birthday = new DateTime(1985, 7, 21)
            });
            this._users.Add(new User()
            {
                No = 3,
                FirstName = "Dai Kang",
                LastName = "Yang",
                Birthday = new DateTime(1987, 1, 17)
            });
        }

        public IEnumerable<User> GetUsers()
        {
            return this._users;
        }

        public DataTable GetUserTable()
        {
            DataRow _row;
            DataTable _table;

            _table = new DataTable();
            _table.Columns.Add("No", typeof(int));
            _table.Columns.Add("LastName", typeof(string));
            _table.Columns.Add("MiddleName", typeof(string));
            _table.Columns.Add("FirstName", typeof(string));
            _table.Columns.Add("NickName", typeof(string));
            _table.Columns.Add("Birthday", typeof(DateTime));

            _row = _table.NewRow();
            _row["No"] = 1;
            _row["FirstName"] = "Chien Ming";
            _row["LastName"] = "Wang";
            _row["Birthday"] = new DateTime(1980, 3, 31);
            _table.Rows.Add(_row);

            _row = _table.NewRow();
            _row["No"] = 2;
            _row["FirstName"] = "Wei Yin";
            _row["LastName"] = "Chen";
            _row["Birthday"] = new DateTime(1985, 7, 21);
            _table.Rows.Add(_row);

            _row = _table.NewRow();
            _row["No"] = 3;
            _row["FirstName"] = "Dai Kang";
            _row["LastName"] = "Yang";
            _row["Birthday"] = new DateTime(1987, 1, 17);
            _table.Rows.Add(_row);

            return _table;
        }

    }
}
