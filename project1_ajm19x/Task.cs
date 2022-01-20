using System;
namespace project1_ajm19x
{
	public class Task
	{
		private string _name;
		private string _description;
		private DateTime _deadline;
		private bool _is_completed;

		public string Name {
			get {
				return _name;
			}
			set {
				_name = value;
			}
		}

		public string Description {
			get {
				return _description;
			}
			set {
				_description = value;
			}
		}
		public DateTime Deadline {
			get {
				return _deadline;
			}
			set {
				_deadline = value;
			}
		}
		public bool IsCompleted {
			get {
				return _is_completed;
			}
			set {
				_is_completed = value;
			}
		}
		public Task() {
			_name = "N/A";
			_description = "N/A";
			_deadline = new DateTime(2001, 06, 03); //my bday
			_is_completed = false;
		}
		public Task(string name, string description, DateTime deadline, bool isComp)
		{
			_name = name;
			_description = description;
			_deadline = deadline;
			_is_completed = isComp;
		}
		public override string ToString() {
			if (_is_completed) {
				return String.Format("{0,-20} {1,-20} {2,-20} {3,-20}",
						  _name,
						  _description,
						  _deadline.ToString("MM/dd/yyyy"),
						  "Complete");
			}
			return String.Format("{0,-20} {1,-20} {2,-20} {3,-20}",
						  _name,
						  _description,
						  _deadline.ToString("MM/dd/yyyy"),
						  "Incomplete");
		}
	}
}

