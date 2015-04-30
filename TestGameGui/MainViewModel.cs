using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TargetServerCommunicator;
using TargetServerCommunicator.Data;
using TargetServerCommunicator.Servers;
using TestGameGui.Annotations;

namespace TestGameGui
{
	public class MainViewModel: INotifyPropertyChanged
	{
		private string m_ipAddress;
		private int m_port;
		private string m_teamName;
		private string m_selectedGame;
		private IGameServer m_server;

		public MainViewModel()
		{
			TeamName		= "Sqrt Dos";
			Targets		= new ObservableCollection<Target>();
			IpAddress	= "192.168.1.3";
			Port			= 3000;

			Games			= new ObservableCollection<string>();

			ConnectCommand		= new MyCommand(Connect);
			GetTargetsCommand = new MyCommand(GetTargets);
			StartGameCommand	= new MyCommand(StartGame);
			StopGameCommand	= new MyCommand(StopGame);
		}

		private void Connect()
		{
			m_server = GameServerFactory.Create(GameServerType.WebClient, TeamName, IpAddress, Port);
			m_server.StopRunningGame();

			var games = m_server.RetrieveGameList();
			foreach(var game in games)
			{
				Games.Add(game);
			}
		}
		/// <summary>
		/// Retrieves the list of targets for the selected game.
		/// </summary>
		private void GetTargets()
		{
			if(m_server == null)
				return;

			if(SelectedGame == null)
				return;

			var targets = m_server.RetrieveTargetList(SelectedGame);
			foreach(var target in targets)
			{
				// Translate the GameServerCommunicatorTarget into your own...
				// Then add to your own collection of targets bound by the View.
				Targets.Add(target);
			}
		}
		private void StartGame()
		{
			if (m_server == null)
				return;

			if (SelectedGame == null)
				return;

			m_server.StartGame(SelectedGame);
		}
		private void StopGame()
		{
			if (m_server == null)
				return;

			if (SelectedGame == null)
				return;

			m_server.StopRunningGame();
		}


		public ObservableCollection<Target> Targets { get; set; }
		public string IpAddress
		{
			get { return m_ipAddress; }
			set
			{
				if(value == m_ipAddress)
				{
					return;
				}
				m_ipAddress = value;
				OnPropertyChanged();
			}
		}
		public int Port
		{
			get { return m_port; }
			set
			{
				if(value == m_port)
				{
					return;
				}
				m_port = value;
				OnPropertyChanged();
			}
		}
		public string TeamName
		{
			get { return m_teamName; }
			set
			{
				if(value == m_teamName)
				{
					return;
				}
				m_teamName = value;
				OnPropertyChanged();
			}
		}
		public ObservableCollection<string> Games { get; set; }
		public string SelectedGame
		{
			get { return m_selectedGame; }
			set
			{
				if(value == m_selectedGame)
				{
					return;
				}
				m_selectedGame = value;
				OnPropertyChanged();
			}
		}
		public ICommand ConnectCommand { get; set; }
		public ICommand GetTargetsCommand { get; set; }
		public ICommand StartGameCommand { get; set; }
		public ICommand StopGameCommand { get; set; }
		
		
		public event PropertyChangedEventHandler PropertyChanged;
		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if(handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
