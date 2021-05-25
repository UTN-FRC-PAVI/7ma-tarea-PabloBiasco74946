using Microsoft.Reporting.WinForms;
using ProyectoBase.AccesoADatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoBase
{
    public partial class reporteListadoCursos : Form
    {
        public reporteListadoCursos()
        {
            InitializeComponent();
        }

        private void reporteListadoCursos_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            DataTable tabla = new DataTable();
            tabla = AD_Cursos.ObtenerListadoDeCursos();

            ReportDataSource ds = new ReportDataSource("DatosCursos", tabla);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(ds);
            reportViewer1.LocalReport.Refresh();
        }
    }
}
