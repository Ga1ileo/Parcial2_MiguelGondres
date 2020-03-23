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
        
        Llamadas llamadas = new Llamadas();
        public List<LlamadasDetalle> Detalle { get; set; }
        
        public Rparcial()
        {
            InitializeComponent();
            this.DataContext = llamadas;
            this.Detalle = new List<LlamadasDetalle>();
            LlamadaIdTextbox.Text = "0";
           
        }

        private void CargarGrid()
        {
            DetalleDatagrid.ItemsSource = null;
            DetalleDatagrid.ItemsSource = this.Detalle;
        }

        private void Actualizar()
        {
            this.DataContext = null;
            this.DataContext = llamadas;
        }
        private void Limpiar()
        {
            LlamadaIdTextbox.Text = "0";
            DescripcionTextbox.Text = string.Empty;


            this.Detalle = new List<LlamadasDetalle>();
            Actualizar();
        }

        private bool ExisteEnBaseDatos()
        {
            Llamadas llamada = LlamadaDetalleBll.Buscar(Convert.ToInt32(LlamadaIdTextbox.Text));
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
            if (string.IsNullOrWhiteSpace(LlamadaIdTextbox.Text) || LlamadaIdTextbox.Text == "0")
                paso = LlamadaDetalleBll.Guardar(llamadas);
            else
            {
                if (!ExisteEnBaseDatos())
                {
                    MessageBox.Show("No se puede modificar", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                paso = LlamadaDetalleBll.Modificar(llamadas);
            }

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado!!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No se puede Guardar", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(LlamadaIdTextbox.Text, out id);

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
            Llamadas anterior = LlamadaDetalleBll.Buscar(Convert.ToInt32(LlamadaIdTextbox.Text));

            if (anterior != null)
            {
                llamadas = anterior;
                Actualizar();
            }
            else
            {
                MessageBox.Show("llamadas no encontrada");
            }
        }

        private void MasButtonProblema_Click(object sender, RoutedEventArgs e)
        {
            llamadas.Detalle.Add(new LlamadasDetalle(Convert.ToInt32(LlamadaIdTextbox.Text), ProblemaTextBox.Text, SolucionTextBox.Text));
            Actualizar();
            ProblemaTextBox.Focus();

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
