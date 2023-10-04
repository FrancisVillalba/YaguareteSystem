using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using YaguareteSystem.Clases;

namespace YaguareteSystem.pdf
{
    public class ImprimirCheque
    {
        ChequeBLL ch = new ChequeBLL();
        NumerosEnLetras n = new NumerosEnLetras();
        public byte[] generaPdfCheque(int codCheque)
        {
            MemoryStream ms = new MemoryStream();
            Document document = new Document();
            document.SetPageSize(PageSize.A4);
            document.SetMargins(12, 12, 8, 12);
            PdfWriter pw = PdfWriter.GetInstance(document, ms);
            DataTable dt = ch.RetornaDatosImpresionCheque(codCheque);

            document.Open();

            foreach (DataRow row2 in dt.Rows)
            {
                string letras = n.Convertir(row2["montoCheque"].ToString(), true, row2["codigoMoneda"].ToString());
                string monto = "";
                int canLetras = letras.Length;

                for (canLetras = 1; canLetras <= 105; canLetras++)
                {
                    letras = letras + "-";
                    canLetras = letras.Length;
                }

                if (row2["codigoMoneda"].ToString() == "USD")
                {
                    // monto = row2["montoCheque"].ToString();
                    String[] Num = row2["montoCheque"].ToString().Split('.');
                    if (Num.Length == 1)
                    {
                        monto = Num[0].ToString();
                        monto = Convert.ToInt32(monto).ToString("N2");
                    }
                    else
                    {
                        monto = Num[0].ToString();
                        monto = Convert.ToInt32(monto).ToString("N2").Replace(",00", "") + "," + Num[1].ToString();
                    }
                    
                }
                else
                {
                    String[] Num = row2["montoCheque"].ToString().Split(',');
                    monto = Num[0].ToString();
                    monto = Convert.ToInt32(monto).ToString("N2").Replace(",00", "");
                }

                //BaseFont _h2_normal = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
                BaseFont _h2_normal = BaseFont.CreateFont(ConfigurationManager.AppSettings.Get("Archivo_utf-8"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED, true);
                iTextSharp.text.Font h2_normal = new iTextSharp.text.Font(_h2_normal, 10f, iTextSharp.text.Font.NORMAL, new BaseColor(0, 0, 0));

                BaseFont _h3_normal = BaseFont.CreateFont(ConfigurationManager.AppSettings.Get("Archivo_utf-8"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED, true);
                iTextSharp.text.Font h3_normal = new iTextSharp.text.Font(_h3_normal, 12f, iTextSharp.text.Font.NORMAL, new BaseColor(0, 0, 0));

                var lblMontoCheque = new PdfPTable(new float[] { 63f, 36f }) { WidthPercentage = 100 };

                lblMontoCheque.AddCell(new PdfPCell(new Phrase("")) { Border = 0 });
                lblMontoCheque.AddCell(new PdfPCell(new Phrase("***" + monto, h3_normal)) { Border = 0 });

                document.Add(lblMontoCheque);

                var lblEspacioBlanco1 = new PdfPTable(new float[] { 45f, 55f }) { WidthPercentage = 100 };

                lblEspacioBlanco1.AddCell(new PdfPCell(new Phrase("")) { Border = 0 });
                lblEspacioBlanco1.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });

                document.Add(lblEspacioBlanco1);


                var lblFecha = new PdfPTable(new float[] { 40f, 55f }) { WidthPercentage = 100 };

                lblFecha.AddCell(new PdfPCell(new Phrase("")) { Border = 0 });
                lblFecha.AddCell(new PdfPCell(new Phrase(row2["dia"].ToString() + "      " + row2["mes"].ToString() + "         " + row2["anho"].ToString(), h3_normal)) { Border = 0 });

                document.Add(lblFecha);

                var lblEspacioBlanco2 = new PdfPTable(new float[] { 45f, 55f }) { WidthPercentage = 100 };

                lblEspacioBlanco2.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });
                lblEspacioBlanco2.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });

                document.Add(lblEspacioBlanco2);


                var lblProveedor = new PdfPTable(new float[] { 15f, 80f }) { WidthPercentage = 100 };

                lblProveedor.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });
                lblProveedor.AddCell(new PdfPCell(new Phrase(row2["denominacionProveedor"].ToString(), h2_normal)) { Border = 0 });

                document.Add(lblProveedor);

                var lblMontoLetra = new PdfPTable(new float[] { 16f, 66f, 18f }) { WidthPercentage = 100 };

                lblMontoLetra.AddCell(new PdfPCell(new Phrase("")) { Border = 0 });
                lblMontoLetra.AddCell(new PdfPCell(new Phrase(letras, h2_normal)) { Border = 0 });
                lblMontoLetra.AddCell(new PdfPCell(new Phrase("")) { Border = 0 });
                document.Add(lblMontoLetra);
            }

            document.Close();

            byte[] bytesStream = ms.ToArray();

            return bytesStream;


        }
    }
}