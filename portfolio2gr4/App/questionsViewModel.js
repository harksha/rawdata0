define(['knockout', 'jquery'], function (ko, $) {

	function viewModel(params) {
		var self = this; 
		var currentQuestion = ko.observable(QuesItem);
		var showSingleQuestion = ko.observable(false);
		var searchResult = ko.observable([]);
		var answers = ko.observable([]);
		var comments = ko.observable([]);
		var questionFaves = ko.observable([]);
		var currentPage = ko.observable("http://localhost:3133/api/questions/10-0");
		var nextPage = ko.observable("");
		var prevPage = ko.observable("");
		var currentUser = ko.observable(localStorage.userId);
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
			getQuestionComments(currentQuestion().Id);
			getVotes(currentQuestion().Id);
			$(".questions").addClass("col-sm-6");
			var top = $(".single-question").offset().top;
			 $("body").scrollTop(top);
			addanno = currentQuestion().Url.substring(36);
		}
		
		function getQuestionComments(id) {
			$.getJSON("api/questions/"+id+"/comments" , function (result) {
				var commms = [];
				if (result.length > 0){
					for (var i = 0; i < result.length; i++) {
						//console.log(result[i]);
					}
				}
			});
		}

		function getVotes(id) {
			$.getJSON("api/questions/" + id + "/votes", function (result) {
				var faves = [];
				if (result.length >= 1) {
					for (var i = 0; i < result.length; i++) {
						var vote = new VoteItem(result[i]);
						console.log(vote); 
						if (vote.Type == 5) { 
							faves.push(vote);
						}
						
					}
				}
				questionFaves(faves);
				console.log(faves);
			});
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

		function fave() {
			console.log(currentUser());
			$.ajax({
				type: "POST",
				url: "api/questions/" + currentQuestion().Id + "/votes",
				data: ko.toJSON({ VoteType: 5, PostId: currentQuestion().Id, UserId:currentUser()  }),
				contentType: "application/json; charset=utf-8",
				success: function (result) {
					console.log("fave Added");
				},
				error: function (err) {
					alert(err.status + " - " + err.statusText);
				}
			});
		};

		function goBack() {
			showSingleQuestion(false);
			$(".questions").removeClass("col-sm-6");
		};

		AddData = function () {
			$.ajax({
				type: "POST",
				url: "http://localhost:3133/api/annotations",
				data: ko.toJSON({ Body: this.Body, PostId: addanno }, console.log(addanno)),
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
			comments:comments,
			questionFaves:questionFaves,
			getPrevPage: getPrevPage,
			getNextPage: getNextPage,
			Body: Body,
			AddData: AddData,
			fave:fave
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

	function CommmentItem(data){
		var self=this;
	};
	
	function VoteItem(data) {
		var self = this;
		self.Type = data.VoteType;
		self.PostId = data.PostId;
		self.UserId = data.UserId;
	};

	return viewModel;
});