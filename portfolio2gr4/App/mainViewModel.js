define(['knockout', 'jquery', 'knockout.validation'], function (ko, $, validation) {
	function viewModel() {
		//OBJECTS:
		var menuItems = ["Home","Questions", "Users", "Annotations", "History"];
		var searchOptions = ["Questions", "Users", "Annotation"];
		var searchOption = ko.observable("Choose");

		var currentComponent = ko.observable(menuItems[0]);
		var changeContent = function (component) {
			currentComponent(component);
		},
				viewData = ko.observable([]);
		
		var changeSearchOption = function (option) {
			searchOption(option);
		};

		var searchText = ko.observable("").extend({
			required: true,
			minLength: 3
		});
		var suggestions = ko.observableArray([]);// for searchbar
		var searchResult = ko.observableArray([]);// for page body
		var showSuggestions = ko.observable(false);

		var currentUser = ko.observable(UsersItem);
		var isLoggedIn = ko.observable(false);
		var userId = ko.observable("");

		//UTIL FUNCTIONS:
		isActive = function (menu) {
			return menu === currentComponent();
		};

		var getSuggestions = function (target, event) {
			switch (searchOption()) {
				case "Users":
					//not implemented yet
					break;
				case "Annotations":
					//not implemented yet
					break;
				case "Questions":
					if (searchText.isValid()) { 
						$.getJSON("api/questions/search_title/" + searchText() + "-10-1", function (result) {
							if (result.length >= 1) {
								showSuggestions(true);
								var titles = $.map(result, function (q) {
									return { Title: q.Title, Id: q.Url.substring(36) };
								});
								suggestions(titles);
							}
						});
					}
					break;
				case "History":
					//not implemented yet
					break;
			}
		};
		var searchFor = function (target, event) {
			switch (searchOption()) {
				case "Users":
					//not implemented yet
					break;
				case "Annotations":
					//not implemented yet
					break;
				case "Questions":
					console.log(searchText());
					$.getJSON("api/questions/search/" + searchText(), function (result) {
						if (result.length >= 1) {
							searchResult(result);
							viewData(searchResult);
							changeContent("Questions");
						} else {
							console.log("no result found!");
						}
					});
					break;
				case "History":
					//not implemented yet
					break;
			}
		};

		function goToQuestion(target) {
			id = target.Id;
			console.log(id);
		}

		function checkLogIn() {
			if (localStorage.userId) {
				userId(localStorage.userId);
				isLoggedIn = ko.observable(true);
				getCurrentUser();

			}
			else { isLoggedIn = ko.observable(false); }
		}

		checkLogIn();

		function storeUserId() { 
			isLoggedIn(true);
			localStorage.setItem("userId", userId());
			getCurrentUser();

		}

		function getCurrentUser() {
			$.getJSON("api/users/" + userId(), function (result) {
				if (result) {
					currentUser(new UsersItem(result));
				}
			});
		}

		function logOut() {
			localStorage.removeItem("userId");
			userId("");
			isLoggedIn(false);
		}

		return {
			currentComponent: currentComponent,
			changeContent: changeContent,
			viewData: viewData, 
			menuItems: menuItems,
			isActive: isActive,
			searchText: searchText,
			suggestions: suggestions,
			getSuggestions: getSuggestions,
			showSuggestions: showSuggestions,
			searchFor: searchFor,
			searchResult: searchResult,
			userId: userId,
			storeUserId: storeUserId,
			isLoggedIn: isLoggedIn,
			logOut:logOut,
			currentUser: currentUser,
			searchOptions:searchOptions,
			searchOption: searchOption,
			changeSearchOption: changeSearchOption,
			goToQuestion:goToQuestion

		}
	};

	function UsersItem(data) {
		var self = this;
		self.Url = data.Url;
		self.CreationDate = data.CreationDate;
		self.Name = data.Name;
		self.Location = data.Location;

	};

	return viewModel;
});