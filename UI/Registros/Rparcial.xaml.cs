using Parcial2_MiguelGondres.BLL;
using Parcial2_MiguelGondres.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Parcial2_MiguelGondres.UI.Registros
{
    /// <summary>
    /// Interaction logic for Rparcial.xaml
    /// </summary>
    public partial class Rparcial : Window
    {
        public List<LlamadaDetalle> Detalle { get; set; }
        Llamada llamada = new Llamada();
        
        public Rparcial()
        {
            InitializeComponent();
            this.DataContext = llamada;
            IdTextBox.Text = "0";
           
        }

        private void CargarGrid()
        {
            this.DataContext = null;
            this.DataContext = llamada;
        }
        private void Limpiar()
        {
            IdTextBox.Text = "0";
            DescripcionTextbox.Text = string.Empty;
            ProblemaTextBox.Text = string.Empty;
            SolucionTextBox.Text = string.Empty;

            this.Detalle = new List<LlamadaDetalle>();
            CargarGrid();
        }

        private bool ExisteEnBaseDatos()
        {
            llamada = LlamadaBll.Buscar(Convert.ToInt32(IdTextBox.Text));

            return (llamada != null);
        }
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if (string.IsNullOrWhiteSpace(IdTextBox.Text))
                paso = LlamadaBll.Guardar(llamada);
            else
            {
                if (!ExisteEnBaseDatos())
                {
                    MessageBox.Show("No ha guardado");
                    return;
                }
                paso = LlamadaBll.Modificar(llamada);
            }

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado");
            }
            else
            {
                MessageBox.Show("No Guardo");
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            id = Convert.ToInt32(IdTextBox.Text);

            Limpiar();

            if (LlamadaBll.Eliminar(id))
                MessageBox.Show("Eliminado");
            else
                MessageBox.Show(IdTextBox.Text, "No se pudo eliminar");
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Llamada anterior = LlamadaBll.Buscar(Convert.ToInt32(IdTextBox.Text));

            if(anterior != null)
            {
                llamada = anterior;
                CargarGrid();
            }
            else
            {
                MessageBox.Show("No lo encuentro");
            }
        }

        private void MasButtonProblema_Click(object sender, RoutedEventArgs e)
        {
            llamada.LlamadaDetalle.Add(new LlamadaDetalle(Convert.ToInt32(IdTextBox.Text), ProblemaTextBox.Text, SolucionTextBox.Text));
            CargarGrid();
            ProblemaTextBox.Focus();
        }

        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {
            Detalle.RemoveAt(DetalleDatagrid.FrozenColumnCount);
            CargarGrid();
        }
    }
}
