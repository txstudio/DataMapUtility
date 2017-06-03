using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataMapUtility
{
    public class DataMapUtility
    {
        /// <summary>
        /// 將泛型物件集合換成 DataTable 物件
        /// </summary>
        /// <typeparam name="T">集合物件型態</typeparam>
        /// <param name="items">要轉換的集合物件</param>
        /// <returns>轉換後的 DataTable 物件</returns>
        public static DataTable MapToTable<T>(IEnumerable<T> items)
        {

            //取得 T 類別中的成員名稱和屬性型別
            PropertyDescriptorCollection _propertyDescriptors;
            DataTable _table;

            _propertyDescriptors = TypeDescriptor.GetProperties(typeof(T));
            _table = new DataTable();

            //新增欄位名稱和型別
            for (int i = 0; i <= _propertyDescriptors.Count - 1; i++)
            {
                PropertyDescriptor _propertyDescriptor = _propertyDescriptors[i];

                //Nullable(Of ..) 型態的屬性必須要取得指定型別

                if (_propertyDescriptor.PropertyType.ToString().Contains("Nullable") == true)
                {
                    _table.Columns.Add(_propertyDescriptor.Name
                                        , Nullable.GetUnderlyingType(_propertyDescriptor.PropertyType));
                }
                else
                {
                    _table.Columns.Add(_propertyDescriptor.Name
                                        , _propertyDescriptor.PropertyType);
                }
            }

            //逐筆新增資料
            object[] values = new object[_propertyDescriptors.Count];

            //物件為空值的話，回傳僅欄位設定的 DataTable 物件
            if (items == null)
            {
                return _table;
            }

            foreach (T item in items)
            {
                for (int i = 0; i <= values.Length - 1; i++)
                {
                    values[i] = _propertyDescriptors[i].GetValue(item);
                }
                _table.Rows.Add(values);
            }

            return _table;
        }

        /// <summary>
        /// 將 IDataReader 物件轉換成指定泛型物件集合
        /// </summary>
        /// <typeparam name="T">泛型物件</typeparam>
        /// <param name="reader">IDataReader 物件</param>
        /// <returns>轉換後的集合物件</returns>
        public static IEnumerable<T> MapFromReader<T>(IDataReader reader)
        {

            List<T> _items;
            T _template;

            List<string> _columns;
            string _columnName;

            _columns = new List<string>();
            _items = new List<T>();


            int _index = 0;

            while (reader.Read())
            {
                _template = Activator.CreateInstance<T>();

                //先取得 DataReader 的欄位名稱清單
                if ((_columns.Count == 0))
                {
                    for (var index = 0; index <= reader.FieldCount - 1; index++)
                    {
                        _columns.Add(reader.GetName(index));
                    }
                }



                foreach (PropertyInfo _propertyInfo in _template.GetType().GetProperties())
                {
                    _columnName = string.Empty;
                    _columnName = _propertyInfo.Name;

                    //屬性名稱並不存在於 DataReader 的欄位中，處理下一個屬性
                    if (_columns.Contains(_columnName) == false)
                    {
                        continue;
                    }

                    //指定資料行為 DBNull 的話不設定類別物件屬性值

                    if (Object.Equals(reader[_columnName], DBNull.Value) == false)
                    {
                        _propertyInfo.SetValue(_template, reader[_columnName], null);
                    }

                }

                _items.Add(_template);

                _index = _index + 1;
            }

            return _items;
        }

        /// <summary>
        /// 將 DataTable 物件轉換成指定泛型物件集合
        /// </summary>
        /// <typeparam name="T">泛型物件</typeparam>
        /// <param name="table">DataTable 物件</param>
        /// <returns>轉換後的集合物件</returns>
        public static IEnumerable<T> MapFromTable<T>(DataTable table)
        {

            List<T> _items;
            T _template;

            List<string> _columns;
            string _columnName;

            _columns = new List<string>();
            _items = new List<T>();


            foreach (DataRow _dataRow in table.Rows)
            {
                _template = Activator.CreateInstance<T>();

                //先取得 DataTable 的欄位名稱清單
                if ((_columns.Count == 0))
                {
                    for (var index = 0; index <= table.Columns.Count - 1; index++)
                    {
                        _columns.Add(table.Columns[index].ColumnName);
                    }
                }


                foreach (PropertyInfo _propertyInfo in _template.GetType().GetProperties())
                {
                    _columnName = string.Empty;
                    _columnName = _propertyInfo.Name;

                    //屬性名稱並不存在於 DataTable 的欄位中，處理下一個屬性
                    if (_columns.Contains(_columnName) == false)
                    {
                        continue;
                    }

                    //指定資料行為 DBNull 的話不設定類別物件屬性值
                    if (Object.Equals(_dataRow[_columnName], DBNull.Value) == false)
                    {
                        object _value;
                        string _propertyTypeFullName;

                        _propertyTypeFullName = _propertyInfo.PropertyType.FullName;

                        if (_propertyTypeFullName.Contains("Int32") == true)
                        {
                            _value = Convert.ToInt32(_dataRow[_columnName]);
                        }
                        else if (_propertyTypeFullName.Contains("Int64") == true)
                        {
                            _value = Convert.ToInt64(_dataRow[_columnName]);
                        }
                        else
                        {
                            _value = _dataRow[_columnName];
                        }

                        _propertyInfo.SetValue(_template, _value, null);
                    }
                }

                _items.Add(_template);
            }

            return _items;

        }
    }
}
