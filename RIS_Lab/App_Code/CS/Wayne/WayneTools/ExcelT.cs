using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;

public class ExcelToolT
{
    public  Stream RenderDataTableToExcel(DataTable SourceTable)
    {
        HSSFWorkbook workbook = new HSSFWorkbook();
        MemoryStream ms = new MemoryStream();
        var sheet = workbook.CreateSheet();
        var headerRow = sheet.CreateRow(0);

        foreach (DataColumn column in SourceTable.Columns)
            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);

        int rowIndex = 1;

        foreach (DataRow row in SourceTable.Rows)
        {
            var dataRow = sheet.CreateRow(rowIndex);

            foreach (DataColumn column in SourceTable.Columns)
            {
                dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
            }

            rowIndex++;
        }

        workbook.Write(ms);
        ms.Flush();
        ms.Position = 0;

        sheet = null;
        headerRow = null;
        workbook = null;

        return ms;
    }

    public  void RenderDataTableToExcel(DataTable SourceTable, string FileName)
    {
        MemoryStream ms = RenderDataTableToExcel(SourceTable) as MemoryStream;
        FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write);
        byte[] data = ms.ToArray();

        fs.Write(data, 0, data.Length);
        fs.Flush();
        fs.Close();

        data = null;
        ms = null;
        fs = null;
    }

    public  DataTable RenderDataTableFromExcel(Stream ExcelFileStream, string SheetName, int HeaderRowIndex)
    {
        HSSFWorkbook workbook = new HSSFWorkbook(ExcelFileStream);
        var sheet = workbook.GetSheet(SheetName);

        DataTable table = new DataTable();

        var headerRow = sheet.GetRow(HeaderRowIndex);
        int cellCount = headerRow.LastCellNum;

        for (int i = headerRow.FirstCellNum; i < cellCount; i++)
        {
            DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
            table.Columns.Add(column);
        }

        int rowCount = sheet.LastRowNum;

        for (int i = (sheet.FirstRowNum + 1); i < sheet.LastRowNum; i++)
        {
            var row = sheet.GetRow(i);
            DataRow dataRow = table.NewRow();

            for (int j = row.FirstCellNum; j < cellCount; j++)
                dataRow[j] = row.GetCell(j).ToString();
        }

        ExcelFileStream.Close();
        workbook = null;
        sheet = null;
        return table;
    }

    public  DataTable RenderDataTableFromExcel(Stream ExcelFileStream, int SheetIndex, int HeaderRowIndex)
    {
        HSSFWorkbook workbook = new HSSFWorkbook(ExcelFileStream);
        var sheet = workbook.GetSheetAt(SheetIndex);

        DataTable table = new DataTable();

        var headerRow = sheet.GetRow(HeaderRowIndex);
        int cellCount = headerRow.LastCellNum;

        for (int i = headerRow.FirstCellNum; i < cellCount; i++)
        {
            DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
            table.Columns.Add(column);
        }

        int rowCount = sheet.LastRowNum;

        for (int i = (sheet.FirstRowNum + 1); i < sheet.LastRowNum; i++)
        {
            var row = sheet.GetRow(i);
            DataRow dataRow = table.NewRow();

            for (int j = row.FirstCellNum; j < cellCount; j++)
            {
                if (row.GetCell(j) != null)
                    dataRow[j] = row.GetCell(j).ToString();
            }

            table.Rows.Add(dataRow);
        }

        ExcelFileStream.Close();
        workbook = null;
        sheet = null;
        return table;
    }
}