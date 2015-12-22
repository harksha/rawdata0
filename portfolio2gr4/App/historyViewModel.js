define(['knockout', 'jquery'], function (ko, $) {

	function viewModel(params) {
		this.title = "Hello from history";
		var currentUser = ko.observable(localStorage.userId);
		var currentPage = ko.observable("");
		var searchResult = ko.observable([]);
		if (currentUser()) {
			console.log(params);
			if (Object.prototype.toString.call(params) === '[object Array]') {

				console.log("wew");
				currentPage = ko.observable("http://localhost:3133/api/" + currentUser() + "/arg-10-1/historys");
				switchPage();
			} else {
			}
		}

		function switchPage() {
			console.log("sr" );
			$.getJSON(currentPage(), function (result, text, jqXHR) {
				console.log("r", result);
				var next = jqXHR.getResponseHeader('next-page');
				var prev = jqXHR.getResponseHeader('prev-page');
				nextPage(next);
				prevPage(prev);
				var rez = [];
				for (var i = 0; i < result.length; i++) {
					var question = new HistoryItem(result[i]);
					rez.push(question);
				};
				searchResult(rez);
				
			});
		}

		return {
			title: this.title,
			currentUser: currentUser,
			searchResult:searchResult
		}


		function HistoryItem(data) {
			var self = this;
			console.log(data);
			self.Url = data.Url;
			self.userId = data.UserId;
			self.body = data.Body;
			self.searchDate = data.SearchDate;
		};

	}

	
	return viewModel;
});