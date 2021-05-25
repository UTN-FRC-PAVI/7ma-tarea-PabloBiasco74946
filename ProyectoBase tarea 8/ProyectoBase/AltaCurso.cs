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
    public partial class AltaCurso : Form
    {
        public AltaCurso()
        {
            InitializeComponent();
        }

        private void AltaCurso_Load(object sender, EventArgs e)
        {
            CargarFecha();
            ObtenerUltimoIdCurso();   
        }

        private void CargarFecha()
        {
            txtFecha.Text = DateTime.Now.ToShortDateString();
        }

        private void ObtenerUltimoIdCurso()
        {

            int id = AD_Cursos.ObtenerUltimoIdCurso();
            txtNroCurso.Text = (id + 1).ToString();
        }

        private void AltaCurso_Load_1(object sender, EventArgs e)
        {
            CargarFecha();
            ObtenerUltimoIdCurso();
        }

        private void btnBuscarCarrera_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable tablaResultado = AD_Carreras.ObtenerCarrerasXId(int.Parse(txtNumCarrera.Text));
                if (tablaResultado.Rows.Count > 0)
                {
                    txtNombreCarrera.Text = tablaResultado.Rows[0][1].ToString();
                }
                else
                {
                    MessageBox.Show("Carrera inexistente!");
                    txtNumCarrera.Focus();
                    txtNombreCarrera.Text = "";
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al obtener Carrera!");
            }
        }

        private void btnBuscarAlumno_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable tablaResultado = AD_Personas.ObtenerPersonaXDocumento(txtDniAlumno.Text.Trim());
                if(tablaResultado.Rows.Count > 0)
                {
                    txtNombreAlumno.Text = tablaResultado.Rows[0][1].ToString();
                    txtApellidoAlumno.Text = tablaResultado.Rows[0][2].ToString();
                    txtIdPersona.Text = tablaResultado.Rows[0][0].ToString();

                }

                else
                {
                    MessageBox.Show("Alumno no encontrado!");
                    txtNombreAlumno.Focus();
                    txtNombreAlumno.Text = "";
                    txtApellidoAlumno.Text = "";

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error al obtener el Alumno!");
            }
        }

        private void btnAgregarAlumno_Click(object sender, EventArgs e)
        {
            grdAlumnos.Rows.Add(txtIdPersona.Text,txtDniAlumno.Text, txtNombreAlumno.Text, txtApellidoAlumno.Text);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            List<int> listaAlumnos = new List<int>();
            for (int i = 0; i < grdAlumnos.Rows.Count; i++)
            {
                listaAlumnos.Add(int.Parse(grdAlumnos.Rows[i].Cells[0].Value.ToString()));
            }
            bool resultado = AD_Cursos.AltaNuevoCurso(int.Parse(txtNroCurso.Text), txtNombre.Text.Trim(), txtDescripcion.Text.Trim(), int.Parse(txtNumCarrera.Text), listaAlumnos);
            if (resultado)
            {
                MessageBox.Show("Curso dado de alta con éxito!");
            }

            else
            {
                MessageBox.Show("Error al dar de alta!");

            }

        }
    }
}
