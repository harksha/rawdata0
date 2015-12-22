define(['knockout', 'jquery'], function (ko, $) {

	function viewModel(params) {
		var self = this;
		var currentUser = ko.observable(UsersItem);
		var showSingleUser = ko.observable(false);
		var searchResult = ko.observable([]);
		var currentPage = ko.observable("http://localhost:3133/api/users/10-0");
		var nextPage = ko.observable("");
		var prevPage = ko.observable("");

		this.title = "Users";

		if (typeof params === 'object') {
			switchPage();
		} else {
			var rez = [];
			for (var i = 0; i < params().length; i++) {
				var users = new UsersItem(params()[i]);
				rez.push(users);
			}
			searchResult(rez);
		}

		function switchPage() {
			$.getJSON(currentPage(), function (result, text, jqXHR) {
				//console.log(this.xhr().getResponseHeader());
				var next = jqXHR.getResponseHeader('next-page');
				var prev = jqXHR.getResponseHeader('prev-page')
				nextPage(next);
				prevPage(prev);
				console.log(nextPage(), prevPage());
				var rez = [];
				for (var i = 0; i < result.length; i++) {
					var users = new UsersItem(result[i]);
					rez.push(users);
				};
				searchResult(rez);
			});
		}

		function getPrevPage() {
			currentPage = prevPage;
			switchPage();
		}

		function getNextPage() {
			currentPage = nextPage;
			switchPage();
		}

		function getSingleUser(data) {
			currentUser(data);
			showSingleUser(true);
			$("body").scrollTop(0);
			//abc = currentQuestion().Url.substring(36);
		}

        

		function goBack() {
			showSingleUser(false);
		}
       
		return {
			title: this.title,
			searchResult: searchResult,
			currentUser: currentUser,
			showSingleUser: showSingleUser,
			getSingleUser: getSingleUser,
			goBack: goBack,
          
			getPrevPage: getPrevPage,
			getNextPage: getNextPage,
       
		}
	}

	function UsersItem(data) {
		var self = this;
		self.Url = data.Url;
		self.CreationDate = data.CreationDate;
		self.Name = data.Name;
		self.Location = data.Location;
        
	};

    


	return viewModel;
});