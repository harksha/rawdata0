define(['knockout', 'jquery', 'knockout.validation'], function (ko, $, validation) {
	function viewModel() {
		//OBJECTS:
		var menuItems = ["Home","Questions", "Users"];
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
		var userId = ko.observable("").extend({required:true});

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
						
						$.getJSON("api/questions/search_title/" + searchText() + "-10-0", function (result) {
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
					if (currentUser()) { 
						$.ajax({
							type: "POST",
							url: "api/users/" + userId + "/historys",
							data: ko.toJSON({ UserId: currentUser().Id, Body: searchText(), Date: new Date().toLocaleString() }),
							contentType: "application/json; charset=utf-8",
							success: function (result) {
								console.log("fave Added");
							},
							error: function (err) {
								console.log(err.status + " - " + err.statusText);
							}
						});
					}
					
					$.getJSON("api/questions/search/" + searchText() + "-10-1", function (result, text, jqXHR) {
						var next = jqXHR.getResponseHeader('next-page');
						var prev = jqXHR.getResponseHeader('prev-page');
						if (result.length >= 1) {
							searchResult(result);
							viewData( searchResult, next, prev);
							changeContent("Questions");
							showSuggestions(false);
							console.log(result);
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
			$.getJSON("api/questions/"+id, function (result) {
				if (result) {
					var q = new QuesItem(result);
					viewData(q);
					showSuggestions(false);
					changeContent("Questions");
				}
			});
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