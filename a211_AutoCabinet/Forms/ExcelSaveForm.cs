using a211_AutoCabinet.Datas;
using NPOI.HSSF.UserModel;
using NPOI.OpenXmlFormats.Shared;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace a211_AutoCabinet.Forms
{
    public partial class ATMW : Form
    {

        private const string EXCELTYPE_XLS = ".XLS";
        private const string EXCELTYPE_XLSX = ".XLSX";

        // 엑셀 파일 저장
        public void SaveExcelFile(string fileName)
        {
            Task.Run(() =>
            {
                try
                {
                    // 워크북 생성 
                    IWorkbook workbook = null;
                    string ext = Path.GetExtension(fileName).ToUpper(); // 파일의 확장명 가져오기
                    if (ext.Equals(EXCELTYPE_XLS))
                        workbook = new HSSFWorkbook();
                    else if (ext.Equals(EXCELTYPE_XLSX))
                        workbook = new XSSFWorkbook();
                    else
                        return;

                    // 작업 시트 생성
                    // 작업 시트 생성 과정은 Sheet 생성 -> Row 생성 -> Cell 생성 순이다.

                    string SheetName = fileName.Replace(ext.ToLower(), "");
                    string[] SheetNameArr = SheetName.Split('\\');
                    List<string> SheetNameList = new List<string>(SheetNameArr);

                    SheetName = SheetNameList[SheetNameList.Count - 1];

                    ISheet sheet = workbook.CreateSheet(SheetName);
                    IRow row;
                    ICell cell;
                    int colNo = 0;
                    int rowNo = 0;


                    // 해더 정보 쓰기
                    // 해더 Row 생성
                    if ((row = sheet.CreateRow(rowNo)) == null)
                        return;
                    // 리스트뷰의 컬럼 개수 만큼
                    for (int i = 0; i < listViewTagDataView.Columns.Count; i++)
                    {
                        // 0번째 셀 부터 값 넣기
                        cell = row.CreateCell(i);
                        cell.SetCellValue(listViewTagDataView.Columns[i].Text);
                    }

                    if (listViewTagDataView.InvokeRequired)
                    {
                        listViewTagDataView.Invoke(new MethodInvoker(delegate ()
                        {
                            // 각 아이템의 서브아이템들을 셀에다 저장
                            foreach (ListViewItem item in listViewTagDataView.Items)
                            {
                                // 각 아이템마다 Row생성
                                colNo = 0;
                                rowNo++;
                                row = sheet.CreateRow(rowNo);
                                // 열 개수만큼 셀 생성
                                for (int i = 0; i < item.SubItems.Count; i++)
                                {
                                    cell = row.CreateCell(colNo);
                                    cell.SetCellValue(item.SubItems[colNo].Text);
                                    ++colNo;
                                }
                                
                            }
                            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                            {
                                // 워크북에 데이터 쓰기
                                workbook.Write(fs);
                            }
                        }));
                    }
                }
                catch
                {
                    MessageBox.Show("Error");
                }
            });
        }

    }
}
