using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using EnigmaMachine.ViewModel;
using EnigmaMachine.View;

namespace EnigmaMachine
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			EnigmaMachineView app = new EnigmaMachineView();
			EnigmaMachineViewModel context = new EnigmaMachineViewModel();
			app.DataContext = context;
			app.Show();
		}
	}
}
