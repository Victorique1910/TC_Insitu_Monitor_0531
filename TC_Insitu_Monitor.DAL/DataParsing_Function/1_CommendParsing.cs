using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.DAL
{
    public static class CommendParsing
    {
        private const double voltageDiv4096 = 3.29 / 4096;
        public static CommendStruct ParsingByteToDataFormat(byte[] input)
        {
            CommendStruct dataFormatStruct_Data = new CommendStruct();
            if (input != null)
            {
                byte length = input[(int)Enum_CommendInput.Length];
                dataFormatStruct_Data.Board = input[(int)Enum_CommendInput.Board2];
                if (length == 0x1D)
                {                    
                    dataFormatStruct_Data.ADC1 = double.Parse((BitConverter.ToInt16(new byte[] {
                                input[(int)Enum_CommendInput.ADC1_low],
                                input[(int)Enum_CommendInput.ADC1_hight] }, 0) * voltageDiv4096).ToString());
                    dataFormatStruct_Data.ADC2 = double.Parse((BitConverter.ToInt16(new byte[] {
                                input[(int)Enum_CommendInput.ADC2_low],
                                input[(int)Enum_CommendInput.ADC2_hight] }, 0) * voltageDiv4096).ToString());
                    dataFormatStruct_Data.ADC3 = double.Parse((BitConverter.ToInt16(new byte[] {
                                input[(int)Enum_CommendInput.ADC3_low],
                                input[(int)Enum_CommendInput.ADC3_hight] }, 0) * voltageDiv4096).ToString());
                    dataFormatStruct_Data.ADC4 = double.Parse((BitConverter.ToInt16(new byte[] {
                                input[(int)Enum_CommendInput.ADC4_low],
                                input[(int)Enum_CommendInput.ADC4_hight] }, 0) * voltageDiv4096).ToString());
                    dataFormatStruct_Data.ADC5 = double.Parse((BitConverter.ToInt16(new byte[] {
                                input[(int)Enum_CommendInput.ADC5_low],
                                input[(int)Enum_CommendInput.ADC5_hight] }, 0) * voltageDiv4096).ToString());
                    dataFormatStruct_Data.ADC6 = double.Parse((BitConverter.ToInt16(new byte[] {
                                input[(int)Enum_CommendInput.ADC6_low],
                                input[(int)Enum_CommendInput.ADC6_hight] }, 0) * voltageDiv4096).ToString());
                    dataFormatStruct_Data.ADC7 = double.Parse((BitConverter.ToInt16(new byte[] {
                                input[(int)Enum_CommendInput.ADC7_low],
                                input[(int)Enum_CommendInput.ADC7_hight] }, 0) * voltageDiv4096).ToString());
                    dataFormatStruct_Data.ADC8 = double.Parse((BitConverter.ToInt16(new byte[] {
                                input[(int)Enum_CommendInput.ADC8_low],
                                input[(int)Enum_CommendInput.ADC8_hight] }, 0) * voltageDiv4096).ToString());
                    dataFormatStruct_Data.ADC9 = double.Parse((BitConverter.ToInt16(new byte[] {
                                input[(int)Enum_CommendInput.ADC9_low],
                                input[(int)Enum_CommendInput.ADC9_hight] }, 0) * voltageDiv4096).ToString());
                    dataFormatStruct_Data.ADC10 = double.Parse((BitConverter.ToInt16(new byte[] {
                                input[(int)Enum_CommendInput.ADC10_low],
                                input[(int)Enum_CommendInput.ADC10_hight] }, 0) * voltageDiv4096).ToString());
                    dataFormatStruct_Data.ADC11 = double.Parse((BitConverter.ToInt16(new byte[] {
                                input[(int)Enum_CommendInput.ADC11_low],
                                input[(int)Enum_CommendInput.ADC11_hight] }, 0) * voltageDiv4096).ToString());
                    dataFormatStruct_Data.ADC12 = double.Parse((BitConverter.ToInt16(new byte[] {
                                input[(int)Enum_CommendInput.ADC12_low],
                                input[(int)Enum_CommendInput.ADC12_hight] }, 0) * voltageDiv4096).ToString());
                    dataFormatStruct_Data.ADC13 = double.Parse((BitConverter.ToInt16(new byte[] {
                                input[(int)Enum_CommendInput.ADC13_low],
                                input[(int)Enum_CommendInput.ADC13_hight] }, 0) * voltageDiv4096).ToString());
                    dataFormatStruct_Data.ADC14 = double.Parse((BitConverter.ToInt16(new byte[] {
                                input[(int)Enum_CommendInput.ADC14_low],
                                input[(int)Enum_CommendInput.ADC14_hight] }, 0) * voltageDiv4096).ToString());  
                }
            }
            return dataFormatStruct_Data;
        }
    }

    public enum Enum_CommendInput  //修改指令參數
    {
        Type = 0,
        Length = 1,
        Board2 = 2,
        ADC1_hight = 3,
        ADC1_low = 4,
        ADC2_hight = 5,
        ADC2_low = 6,
        ADC3_hight = 7,
        ADC3_low = 8,
        ADC4_hight = 9,
        ADC4_low = 10,
        ADC5_hight = 11,
        ADC5_low = 12,
        ADC6_hight = 13,
        ADC6_low = 14,
        ADC7_hight = 15,
        ADC7_low = 16,
        ADC8_hight = 17,
        ADC8_low = 18,
        ADC9_hight = 19,
        ADC9_low = 20,
        ADC10_hight = 21,
        ADC10_low = 22,
        ADC11_hight = 23,
        ADC11_low = 24,
        ADC12_hight = 25,
        ADC12_low = 26,
        ADC13_hight = 27,
        ADC13_low = 28,
        ADC14_hight = 29,
        ADC14_low = 30,
        ENDMARK = 31,
        END
    }
}
