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
        public Rparcial()
        {
            InitializeComponent();
            this.DataContext = llamadas;
            DescripcionTextbox.Text = "0";

        }

        private void Limpiar()
        {
            LlamadaIdTextbox.Text = "0";
            DescripcionTextbox.Text = string.Empty;

            DetalleDatagrid.ItemsSource = new List<LlamadasDetalle>();
            Actualizar();
        }

        private void Actualizar()
        {
            this.DataContext = null;
            this.DataContext = llamadas;
        }

        private bool ExisteEnBaseDatos()
        {
            llamadas = LlamadaDetalleBll.Buscar(Convert.ToInt32(LlamadaIdTextbox.Text));
            return (llamadas != null);
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
                    MessageBox.Show("No se puede Modificar porque no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
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

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            id = Convert.ToInt32(LlamadaIdTextbox.Text);

            Limpiar();

            if (LlamadaDetalleBll.Eliminar(id))
                MessageBox.Show("Eliminar", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show(LlamadaIdTextbox.Text, "No se puede eliminar una llamada que no existe");
        }

        private void MasButtonProblema_Click(object sender, RoutedEventArgs e)
        {
            llamadas.LlamadasDetalle.Add(new LlamadasDetalle(Convert.ToInt32(LlamadaIdTextbox.Text), ProblemaTextBox.Text, SolucionTextBox.Text));
            Actualizar();
            ProblemaTextBox.Focus();
        }

        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {
            llamadas.LlamadasDetalle.RemoveAt(DetalleDatagrid.FrozenColumnCount);
            Actualizar();
        }
    }
}
