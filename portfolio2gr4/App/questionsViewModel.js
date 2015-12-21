define(['knockout', 'jquery'], function (ko, $) {

	function viewModel(params) {
		var self = this; 
		var currentQuestion = ko.observable(QuesItem);
		var showSingleQuestion = ko.observable(false);
		var searchResult = ko.observable([]);

		this.title = "Hello from questions";

		if (typeof params === 'object') {
			$.getJSON("http://localhost:3133/api/questions/10-1", function (result) {
				var rez = [];
				for (var i = 0; i < result.length; i++) {
					var question = new QuesItem(result[i]); 
					rez.push(question);
				};
				searchResult(rez);
			});
		} else {
			var rez = [];
			for(var i=0; i<params().length; i++){
				var question = new QuesItem(params()[i]);
				rez.push(question);
			}
			searchResult(rez);
		}

		function getSingleQuestion(data) {
			
			currentQuestion(data);
			showSingleQuestion(true);
			console.log(currentQuestion());

		}

		function goBack() {
			showSingleQuestion(false);
		}

		return {
			title:this.title,
			searchResult: searchResult,
			currentQuestion:currentQuestion,
			showSingleQuestion: showSingleQuestion,
			getSingleQuestion: getSingleQuestion,
			goBack:goBack
		}
	}

	function QuesItem(data) {
		var self = this; 
		self.Url = data.Url;
		self.CreationDate = data.CreationDate;
		self.Body = data.Body;
		self.Title = data.Title;
		self.Owner = data.Owner;
		self.Id = data.Url.substring(36);
	};
	
	return viewModel;
});