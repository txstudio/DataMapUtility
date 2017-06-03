# DataMapUtility
將 ADO.Net 資料物件與清單物件轉換方法

### DataMapUtility
> 提供資料物件轉換方法類別庫專案

DataTable、DataReader 轉換成指定型態的集合物件。

集合物件轉換成 DataTable 物件。

支援 Nullable 資料型態轉換，進行集合物件轉換時僅轉換集合物件有的屬性。

靜態方法 | 說明
-|-
MapToTable<T>(IEnumerable<T>) | 將集合物件轉換成 DataTable 物件，若當物件為 Null 時，回傳僅包含物件屬性對應 DataColumn 設定的 DataTable 物件，資料列數量為 0。
MapFromTable<T>(DataTable) | 將 DataTable 轉換成指定型態的集合物件。
MapFromReader<T>(IDataReader) | 將 IDataReader 轉換成指定型態的集合物件。

### DataMapUtility.Test
> 測試 DataMapUtility 類別的測試專案

DataMapUtility 提供的轉換方法可在此專案取得使用範例。

檔案|說明
-|-
Test_MapFromReader.cs|因 IDataReader 物件無法透過初始化方式建立。使用 RhinoMocks 套件 Mock 出 IDataReader 模擬物件進行 MapFromReader 方法測試。
