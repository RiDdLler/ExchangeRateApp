using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json.Linq;

namespace CurrencyExchangeApp
{
	public partial class MainWindow : Window
	{
		private const string APP_ID = "1698e6da8c3d47a79f03032e7ce1df4b";
		private const string ApiUrl = "https://openexchangerates.org/api/latest.json";

		public MainWindow()
		{
			InitializeComponent();
		}

		private async void UpdateCurrencyRatesButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				using (var httpClient = new HttpClient())
				{
					var response = await httpClient.GetAsync($"{ApiUrl}?app_id={APP_ID}");

					if (response.IsSuccessStatusCode)
					{
						var content = await response.Content.ReadAsStringAsync();
						var rates = JObject.Parse(content)["rates"];

						decimal kztToUsd = 1 * rates["KZT"].Value<decimal>();
						usdTextBlock.Text = $"1 доллар = {kztToUsd / rates["USD"].Value<decimal>():F2} тенге";
						eurTextBlock.Text = $"1 евро = {kztToUsd / rates["EUR"].Value<decimal>():F2} тенге";
						rubTextBlock.Text = $"1 рубль = {kztToUsd / rates["RUB"].Value<decimal>():F2} тенге";
						kgsTextBlock.Text = $"1 сом = {kztToUsd / rates["KGS"].Value<decimal>():F2} тенге";
						gbpTextBlock.Text = $"1 фунт = {kztToUsd / rates["GBP"].Value<decimal>():F2} тенге";
						cnyTextBlock.Text = $"1 юань = {kztToUsd / rates["CNY"].Value<decimal>():F2} тенге";
						goldTextBlock.Text = $"1 унция золота = {kztToUsd / rates["XAU"].Value<decimal>():F2} тенге";
					}
					else
					{
						MessageBox.Show("Не удалось получить курсы валют.");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Произошла ошибка: {ex.Message}");
			}
		}
	}
}
