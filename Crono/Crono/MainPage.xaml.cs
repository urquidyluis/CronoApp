using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Crono
{
    public partial class MainPage : ContentPage
    {
        int c_hora = 0;
        int c_minu = 0;
        int c_segu = 0;
        int c_mili = 0;
        int e_hora = 0;
        int e_minu = 0;
        int e_segu = 0;
        int e_mili = 0;
        bool CronoActivo = false;
        bool RelojJugada = false;
        bool rjugada = false;

        int down = 0;
        Color defaultcolor = Color.LightGray;
        Color color = Color.Red;

        int cuarenta = 0;
        int veinti = 0;

        //DateTime tiempo = new DateTime();
        public MainPage()
        {
            InitializeComponent();
            HoraActual();
            ComienzaApp();
            ContadorDown();
        }
        public void ComienzaApp()
        {
            c_hora = 00;
            c_minu = 00;
            c_segu = 00;
            c_mili = 00;
            e_hora = 00;
            e_minu = 12;
            e_segu = 00;
            e_mili = 00;
            CronoActivo = false;
            RelojJugada = false;

            AsignarValores();
        }
        public void ValidarInicio()
        {
            ValidarCrono();
            if (!ValidarEstimado())
                DisplayAlert("Error", "Agrega tiempo a temporizar en 'restante'", "OK");
            else
                ActivaCrono();

        }
        public void ValidarCrono()
        {
            if (t_hora.Text != null)
                c_hora = Convert.ToInt16(t_hora.Text);
            if (t_minu.Text != null)
                c_minu = Convert.ToInt16(t_minu.Text);
            if (t_segu.Text != null)
                c_segu = Convert.ToInt16(t_segu.Text);
            if (t_mili.Text != null)
                c_mili = Convert.ToInt16(t_mili.Text);
            
        }
        public bool ValidarEstimado()
        {
            bool Result;
            if (r_hora.Text != null || r_minu.Text != null || r_segu.Text != null || r_mili.Text != null)
            {
                if (r_hora.Text != null || r_hora.Text != "")
                    e_hora = Convert.ToInt16(r_hora.Text);
                if (r_minu.Text != null || r_minu.Text != "")
                    e_minu = Convert.ToInt16(r_minu.Text);
                if (r_segu.Text != null || r_segu.Text != "")
                    e_segu = Convert.ToInt16(r_segu.Text);
                if (r_mili.Text != null || r_mili.Text != "")
                    e_mili = Convert.ToInt16(r_mili.Text);
                Result = true;
            }
            else
            {
                Result = false;
            }
            return Result;
        }
        public void ActivaCrono()
        {
            if (CronoActivo == false)
            {
                CronoActivo = true;
                RelojJugada = false;
                Device.StartTimer(TimeSpan.FromMilliseconds(93), AvanzaCrono);
                AvanzaCrono();
                Device.StartTimer(TimeSpan.FromMilliseconds(93), RetrocedeCrono);
                RetrocedeCrono();
                Device.StartTimer(TimeSpan.FromMilliseconds(1), AvanzaCuarenta);
                AvanzaCuarenta();
            }
            else
            {
                CronoActivo = false;
                RelojJugada = true;
                Device.StartTimer(TimeSpan.FromMilliseconds(93), AvanzaCrono);
                AvanzaCrono();
                Device.StartTimer(TimeSpan.FromMilliseconds(93), RetrocedeCrono);
                RetrocedeCrono();
                down++;
                ContadorDown();
                cuarenta = 42;
                Device.StartTimer(TimeSpan.FromSeconds(1), AvanzaCuarenta);
                AvanzaCuarenta();
            }
            
        }
        public void AsignarValores()
        {
            t_hora.Text = c_hora.ToString();
            t_minu.Text = c_minu.ToString();
            t_segu.Text = c_segu.ToString();
            t_mili.Text = c_mili.ToString();
            r_hora.Text = e_hora.ToString();
            r_minu.Text = e_minu.ToString();
            r_segu.Text = e_segu.ToString();
            r_mili.Text = e_mili.ToString();
        }
        bool AvanzaCrono()
        {
            c_mili++;
            if (c_mili == 10) { c_mili = 0; c_segu++; }
            if (c_segu == 60) { c_segu = 0; c_minu++; }
            if (c_minu == 60) { c_minu = 0; c_hora++; }
            //if (c_mili < 10) { c_mili = "0" + c_mili; }
            //if (c_seconds < 10) { c_seconds = `0` +c_seconds; }
            //if (c_minutes < 10) { c_minutes = `0` +c_minutes; }
            //if (c_hours < 10) { c_hours = `0` +c_hours; }
            t_hora.Text = c_hora.ToString();
            t_minu.Text = c_minu.ToString();
            t_segu.Text = c_segu.ToString();
            t_mili.Text = c_mili.ToString();
            //DateTime dateTime = DateTime.Now;
            //etiquetaHoraActual.Text = dateTime.ToString("T");
            //Console.WriteLine(dateTime);
            if (CronoActivo == true)
                return true;
            else return false;
        }
        bool RetrocedeCrono()
        {
            e_mili--;
            if (e_mili < 0) { e_mili = 9; e_segu--; }
            if (e_segu < 0) { e_segu = 59; e_minu--; }
            if (e_minu < 0) { e_minu = 59; e_hora--; }
            if (e_hora == 00 && e_minu == 0 && e_segu == 0 && e_mili == 0) { return false; }
            //if (c_mili < 10) { c_mili = "0" + c_mili; }
            //if (c_seconds < 10) { c_seconds = `0` +c_seconds; }
            //if (c_minutes < 10) { c_minutes = `0` +c_minutes; }
            //if (c_hours < 10) { c_hours = `0` +c_hours; }
            r_hora.Text = e_hora.ToString();
            r_minu.Text = e_minu.ToString();
            r_segu.Text = e_segu.ToString();
            r_mili.Text = e_mili.ToString();
            //DateTime dateTime = DateTime.Now;
            //etiquetaHoraActual.Text = dateTime.ToString("T");
            //Console.WriteLine(dateTime);
            if (CronoActivo == true)
                return true;
            else return false;
        }

        bool AvanzaCuarenta()
        {
            etiquetaCuarenta.Text = "40";
            cuarenta--;
            if (cuarenta == 0) cuarenta = 0;
            if (cuarenta <= 40) etiquetaCuarenta.Text = cuarenta.ToString();
            if (RelojJugada == true)
                return true;
            else return false;
        }
        private void btn40_Clicked(object sender, EventArgs e)
        {
            if (RelojJugada == false)
            {
                RelojJugada = true;
                cuarenta = 42;
                Device.StartTimer(TimeSpan.FromSeconds(1), AvanzaCuarenta);
                AvanzaCuarenta();
            }
            else
            {
                RelojJugada = false;
                Device.StartTimer(TimeSpan.FromSeconds(1), AvanzaCuarenta);
                AvanzaCuarenta();
            }
                
        }
        bool AvanzaVeinti()
        {
            etiquetaVeinti.Text = "25";
            veinti--;
            if (veinti == 0) veinti = 0;
            if (veinti <= 40) etiquetaVeinti.Text = veinti.ToString();
            if (RelojJugada == true)
                return true;
            else return false;
        }
        private void btn25_Clicked(object sender, EventArgs e)
        {
            if (RelojJugada == false)
            {
                RelojJugada = true;
                veinti = 25;
                Device.StartTimer(TimeSpan.FromSeconds(1), AvanzaVeinti);
                AvanzaCuarenta();
            }
            else
            {
                RelojJugada = false;
                Device.StartTimer(TimeSpan.FromSeconds(1), AvanzaVeinti);
                AvanzaCuarenta();
            }

        }

        public void HoraActual()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), OnTimer);
            OnTimer();
            //{
            //    etiquetaHoraActual.Text = horaActual.ToString("T");  
            //}
        }
        bool OnTimer()
        {
            DateTime dateTime = DateTime.Now;
            etiquetaHoraActual.Text = dateTime.ToString("T");
            //Console.WriteLine(dateTime);
            return true;
        }
        public void HoraJuego()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), TiempoJuego);
            TiempoJuego();
        }
        bool TiempoJuego()
        {
            if (rjugada == false)
            {            
                rjugada = true;
                int tiempo = 0;
                Convert.ToDateTime(tiempo);
                etiquetaHoraJuego.Text = tiempo.ToString();
                tiempo++;
            }
            else
            {
                return true;
            }
            return true;
        }
        private void btnIniciar_Clicked(object sender, EventArgs e)
        {
            ValidarInicio();
        }
        private void checkRestante_Clicked(object sender, EventArgs e)
        {
            ValidarEstimado();
            AsignarValores();
        }
        private void checkTiempo_Clicked(object sender, EventArgs e)
        {
            ValidarCrono();
            AsignarValores();
        }
    
        public void ContadorDown()
        {
            if (down == 0)
            {
                btnk_Clicked(null, null);
                //btnk.Clicked += btnk_Clicked;
            }
            if (down == 1)btn1_Clicked(null, null);
            if (down == 2)btn2_Clicked(null, null);
            if (down == 3)btn3_Clicked(null, null);
            if (down == 4)btn4_Clicked(null, null);
            if (down == 5)btnk_Clicked(null, null);
        }
        private void btn1_Clicked(object sender, EventArgs e)
        {
            down = 1;
            btn1.BackgroundColor = color;
            btn2.BackgroundColor = defaultcolor;
            btn3.BackgroundColor = defaultcolor;
            btn4.BackgroundColor = defaultcolor;
            btnk.BackgroundColor = defaultcolor;

        }
        private void btn2_Clicked(object sender, EventArgs e)
        {
            down = 2;
            btn1.BackgroundColor = defaultcolor;
            btn2.BackgroundColor = color;
            btn3.BackgroundColor = defaultcolor;
            btn4.BackgroundColor = defaultcolor;
            btnk.BackgroundColor = defaultcolor;
        }
        private void btn3_Clicked(object sender, EventArgs e)
        {
            down = 3;
            btn1.BackgroundColor = defaultcolor;
            btn2.BackgroundColor = defaultcolor;
            btn3.BackgroundColor = color;
            btn4.BackgroundColor = defaultcolor;
            btnk.BackgroundColor = defaultcolor;
        }
        private void btn4_Clicked(object sender, EventArgs e)
        {
            down = 4;
            btn1.BackgroundColor = defaultcolor;
            btn2.BackgroundColor = defaultcolor;
            btn3.BackgroundColor = defaultcolor;
            btn4.BackgroundColor = color;
            btnk.BackgroundColor = defaultcolor;
        }
        private void btnk_Clicked(object sender, EventArgs e)
        {
            down = 0;
            btn1.BackgroundColor = defaultcolor;
            btn2.BackgroundColor = defaultcolor;
            btn3.BackgroundColor = defaultcolor;
            btn4.BackgroundColor = defaultcolor;
            btnk.BackgroundColor = color;
        }

        private void btnReset_Clicked(object sender, EventArgs e)
        {
            ComienzaApp();
        }
    }
}
