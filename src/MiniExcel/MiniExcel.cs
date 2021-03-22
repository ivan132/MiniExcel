﻿namespace MiniExcelLibs
{
    using MiniExcelLibs.OpenXml;
    using System.Linq;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System;
    using MiniExcelLibs.Csv;

    public static partial class MiniExcel
    {
        private readonly static UTF8Encoding Utf8WithBom = new System.Text.UTF8Encoding(true);
       

        public static void SaveAs(this Stream stream, object value, string startCell = "A1", bool printHeader = true, ExcelType excelType = ExcelType.Xlsx)
        {
            switch (excelType)
            {
                case ExcelType.Csv:
                    CsvImpl.SaveAs(stream, value);
                    break;
                case ExcelType.Xlsx:
                    SaveAsImpl(stream, GetCreateXlsxInfos(value, startCell, printHeader));
                    //stream.Position = 0;
                    break;
                default:
                    throw new NotSupportedException($"Extension : {excelType} not suppprt");
            }
        }

        public static void SaveAs(string filePath, object value, string startCell = "A1", bool printHeader = true)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();
            switch (extension)
            {
                case ".csv":
                    CsvImpl.SaveAs(filePath, value);
                    break;
                case ".xlsx":
                    SaveAsImpl(filePath, GetCreateXlsxInfos(value, startCell, printHeader));
                    break;
                default:
                    throw new NotSupportedException($"Extension : {extension} not suppprt");
            }
        }

        public static IEnumerable<T> Query<T>(this Stream stream) where T : class, new()
        {
            return QueryImpl<T>(stream);
        }

        public static T QueryFirst<T>(this Stream stream) where T : class, new()
        {
            return QueryImpl<T>(stream).First();
        }

        public static T QueryFirstOrDefault<T>(this Stream stream) where T : class, new()
        {
            return QueryImpl<T>(stream).FirstOrDefault();
        }

        public static T QuerySingle<T>(this Stream stream) where T : class, new()
        {
            return QueryImpl<T>(stream).Single();
        }

        public static T QuerySingleOrDefault<T>(this Stream stream) where T : class, new()
        {
            return QueryImpl<T>(stream).SingleOrDefault();
        }

        public static IEnumerable<dynamic> Query(this Stream stream, bool useHeaderRow = false)
        {
            return new ExcelOpenXmlSheetReader().QueryImpl(stream, useHeaderRow);
        }

        public static dynamic QueryFirst(this Stream stream, bool useHeaderRow = false)
        {
            return new ExcelOpenXmlSheetReader().QueryImpl(stream, useHeaderRow).First();
        }

        public static dynamic QueryFirstOrDefault(this Stream stream, bool useHeaderRow = false)
        {
            return new ExcelOpenXmlSheetReader().QueryImpl(stream, useHeaderRow).FirstOrDefault();
        }

        public static dynamic QuerySingle(this Stream stream, bool useHeaderRow = false)
        {
            return new ExcelOpenXmlSheetReader().QueryImpl(stream, useHeaderRow).Single();
        }

        public static dynamic QuerySingleOrDefault(this Stream stream, bool useHeaderRow = false)
        {
            return new ExcelOpenXmlSheetReader().QueryImpl(stream, useHeaderRow).SingleOrDefault();
        }
    }
}
