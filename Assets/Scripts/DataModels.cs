using System.Collections;
using System.Collections.Generic;

namespace DataModels {
	[System.Serializable]
	public class Store  {

		public int id;
		public string title;
		public string address;
		public string manager;

	}

	[System.Serializable]
	public class Survey {

		public int id;
		public string title;
		public string date;
		public string comments;
		public Store store;
		public Manager manager;
		public List<Task> allTasks;

		public Survey(){
			store = new Store();
			manager = new Manager();
			allTasks = new List<Task>();
		}

	}

	[System.Serializable]
	public class Task {

		public int id;
		public string title;
		public string description;
		public string date;

	}

	[System.Serializable]
	public class Manager {

		public int id;
		public string title;
		public string description;
		public string nationality;
		public string phone;
		public string email;

	}
}
