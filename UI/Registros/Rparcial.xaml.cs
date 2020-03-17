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
        
        Llamadas llamada = new Llamadas();
        public List<LlamadasDetalle> Detalle { get; set; }
        
        public Rparcial()
        {
            InitializeComponent();
            this.DataContext = llamada;
            this.Detalle = new List<LlamadasDetalle>();
            IdTextBox.Text = "0";
           
        }

        private void CargarGrid()
        {
            DetalleDatagrid.ItemsSource = null;
            DetalleDatagrid.ItemsSource = this.Detalle;
        }

        private void Actualizar()
        {
            this.DataContext = null;
            this.DataContext = llamada;
        }
        private void Limpiar()
        {
            IdTextBox.Text = "0";
            DescripcionTextbox.Text = string.Empty;


            this.Detalle = new List<LlamadasDetalle>();
            CargarGrid();
        }

        private bool ExisteEnBaseDatos()
        {
            Llamadas llamada = LlamadaDetalleBll.Buscar(Convert.ToInt32(IdTextBox.Text));
            return (llamada != null);
        }

        private bool Validar()
        {
            bool paso = true;

            if (string.IsNullOrEmpty(DescripcionTextbox.Text))
            {
                MessageBox.Show("Este campo no puede estar vacio");
                paso = false;
                DescripcionTextbox.Focus();
            }

            if(this.Detalle.Count == 0)
            {
                MessageBox.Show("Debe agregar lo indicado");
                paso = false;
            }

            return paso;
        }
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if (!Validar())
                return;

            if (string.IsNullOrWhiteSpace(IdTextBox.Text) || IdTextBox.Text == "0")
                paso = LlamadaDetalleBll.Guardar(llamada);
            else
            {
                if (!ExisteEnBaseDatos())
                {
                    MessageBox.Show("No pudo ser Guardado");
                    return;
                }
                paso = LlamadaDetalleBll.Modificar(llamada);
            }

            if (paso)
            {
                MessageBox.Show("Guardado");
                Limpiar();
            }
            else
            {
                MessageBox.Show("No Guardado");
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(IdTextBox.Text, out id);

            Limpiar();

            if (LlamadaDetalleBll.Eliminar(id))
            {
                MessageBox.Show("Eliminado");
            }
            else
            {
                MessageBox.Show("No se pudo eliminar");
            }    
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(IdTextBox.Text, out id);

            llamada = LlamadaDetalleBll.Buscar(id);

            if(llamada != null)
            {
                this.DataContext = llamada;
                Actualizar();
            }
        }

        private void MasButtonProblema_Click(object sender, RoutedEventArgs e)
        {
            if (DetalleDatagrid.Items != null)
                this.Detalle = (List<LlamadasDetalle>)DetalleDatagrid.ItemsSource;

            this.Detalle.Add(new LlamadasDetalle()
            {
                LlamadaDetalleId = 0,
                Problemas = ProblemaTextBox.Text,
                Solucion = SolucionTextBox.Text,
            });
            CargarGrid();
            ProblemaTextBox.Text = string.Empty;
            SolucionTextBox.Text = string.Empty;

        }

        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {
            if(DetalleDatagrid.Items.Count > 0 && DetalleDatagrid.SelectedItem != null)
            {
                this.Detalle.RemoveAt(DetalleDatagrid.SelectedIndex);
                CargarGrid();
            }
        }

    }
}
