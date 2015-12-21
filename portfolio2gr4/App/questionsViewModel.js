define(['knockout', 'jquery'], function (ko, $) {

	function viewModel(params) {
		var self = this; 
		var currentQuestion = ko.observable(QuesItem);
		var showSingleQuestion = ko.observable(false);
		var searchResult = ko.observable([]);
		var answers = ko.observable([]);
		var currentPage = ko.observable("http://localhost:3133/api/questions/10-0");
		var nextPage = ko.observable("");
		var prevPage = ko.observable("");
	    Body = ko.observable(""),

		this.title = "Hello from questions";

		if (typeof params === 'object') {
			switchPage();
		} else {
			var rez = [];
			for(var i=0; i<params().length; i++){
				var question = new QuesItem(params()[i]);
				rez.push(question);
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
					var question = new QuesItem(result[i]);
					rez.push(question);
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

		function getSingleQuestion(data) {
			currentQuestion(data);
			showSingleQuestion(true); 
			getAnswers(currentQuestion().Id);
			$("body").scrollTop(0);
			abc = currentQuestion().Url.substring(36);
		}

		function getAnswers(id) {
			$.getJSON("api/questions/"+id+"/answers" , function (result) {
				var ans = [];
				if (result.length >= 1) {
					for (var i = 0; i < result.length; i++) {
						var answer = new AnswerItem(result[i]);
						ans.push(answer);
					}
				}
				answers(ans);
			});
		}

		function goBack() {
			showSingleQuestion(false);
		}
		AddData = function () {

		    $.ajax({
		        type: "POST",
		        url: "http://localhost:3133/api/annotations",
		        data: ko.toJSON({ Body: this.Body, PostId: abc }, console.log(abc)),
		        contentType: "application/json; charset=utf-8",
		        success: function (result) {
		           // anno.push(new annoItem(result));
		            alert("Annotation Added");
		        },
		        error: function (err) {
		            alert(err.status + " - " + err.statusText);
		        }
		    });
		};
		return {
			title:this.title,
			searchResult: searchResult,
			currentQuestion:currentQuestion,
			showSingleQuestion: showSingleQuestion,
			getSingleQuestion: getSingleQuestion,
			goBack: goBack,
			answers: answers,
			getPrevPage: getPrevPage,
			getNextPage: getNextPage,
			Body: Body,
			AddData: AddData,
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

	function AnswerItem(data) {
		var self = this;
		self.Body = data.Body;
		self.CreationDate = data.CreationDate;
		self.Owner = data.Owner;
		self.Score = data.score;
		self.Id = data.Url.substring(20);
	};

	
	return viewModel;
});