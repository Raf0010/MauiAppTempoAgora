using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try {
                if (!string.IsNullOrEmpty(txt_Cidade.Text)) {
                    Tempo? t = await DataService.GetPrevisao(txt_Cidade.Text);
                    if (t != null)
                    {
                        string dados_previsao = "";
                        dados_previsao = $"Descrição: {t.description} \n" +
                            $"Latitude: {t.lat} \n" +
                            $"Longitude: {t.lon} \n" +
                            $"Temperatura máxima: {t.temp_max} \n" +
                            $"Temperatura mínima: {t.temp_min} \n" +
                            $"Visibilidade: {t.visibility} \n" +
                            $"Velocidade do Vento: {t.speed} \n" +
                            $"Nascer do Sol: {t.sunrise} \n" +
                            $"Pôr do Sol: {t.sunset} \n";
                        lbl_res.Text = dados_previsao;
                    }
                    else {
                        lbl_res.Text = "Sem dados de previsão";
                    }
                } else
                {
                    lbl_res.Text = "Preencha a cidade";
                }

            }
            catch (Exception ex){
                await DisplayAlert("Algo deu errado!", ex.Message, "Okay!");
            }
        }
    }

}
