define(['knockout', 'jquery'], function (ko, $) {

	function viewModel(params) {
		var self = this; 
		var currentQuestion = ko.observable(QuesItem);
		var showSingleQuestion = ko.observable(false);
		var searchResult = ko.observable([]);
		var answers = ko.observable([]);
		var comments = ko.observable([]);
		var questionFaves = ko.observable([]);
		var annotation = ko.observable(AnnotationItem);
		var annotationExist = ko.observable(false);
		var currentPage = ko.observable("");
		var nextPage = ko.observable("");
		var prevPage = ko.observable("");
		var currentUser = ko.observable(localStorage.userId);
		Body = ko.observable(""),

		this.title = "Questions";

		if (typeof params === 'object') { 
			if (params.Id) {
				currentQuestion(params);
				searchResult(params);
				showSingleQuestion(true);
			}
			else {
				currentPage = ko.observable("http://localhost:3133/api/questions/10-0");
				switchPage();
			}
			
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
				var next = jqXHR.getResponseHeader('next-page');
				var prev = jqXHR.getResponseHeader('prev-page');
				nextPage(next);
				prevPage(prev); 
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
			$(".fav-glyph").removeClass("glyphicon-star");
			$(".fav-glyph").addClass("glyphicon-star-empty");
			currentQuestion(data);
			showSingleQuestion(true); 
			getAnswers(currentQuestion().Id);
			getAnnotation(currentUser(), currentQuestion().Id);
			getQuestionComments(currentQuestion().Id);
			getVotes(currentQuestion().Id);
			$(".questions").addClass("col-sm-6");
			var top = $(".single-question").offset().top;
			$("body").scrollTop(top);
			addanno = currentQuestion().Url.substring(36);
		}
		
		function getQuestionComments(id) {
			$.getJSON("api/questions/"+id+"/comments" , function (result) {
				var comms = [];
				if (result.length > 0){
					for (var i = 0; i < result.length; i++) {
						comms.push(new CommentItem(result[i]));
					}
				}
				comments(comms);
				console.log(comments());
				
			});
		}

		function getVotes(id) {
			$.getJSON("api/questions/" + id + "/votes", function (result) {
				var faves = [];
				if (result.length >= 1) {
					for (var i = 0; i < result.length; i++) {
						var vote = new VoteItem(result[i]);
						if (vote.Type == 5) { 
							faves.push(vote);
						}
					}
				}
				questionFaves(faves); 
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

		function getAnnotation(uid, pid) {
			var promise = $.getJSON("api/annotations/" + pid + "/" + uid);
			promise.done(function(result){
				if (result) {
					console.log(result);
					var anno = new AnnotationItem(result);
					annotation(anno);
					annotationExist(true);
				}
			})
			.fail(function() {
				annotationExist(false);
				annotation();
			});
		
			
		}

		function fave() {
			var qid = currentQuestion().Id;
			var uid = currentUser();
			$(".fav-glyph").addClass("glyphicon-star");
			$(".fav-glyph").removeClass("glyphicon-star-empty");
			questionFaves().push({"VoteType":5, "PostId":qid, "UserId":uid});
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
			$(".anno-modal.add").modal('toggle');
			$.ajax({
				type: "POST",
				url: "http://localhost:3133/api/annotations",
				data: ko.toJSON({ Body: this.Body, PostId: addanno, UserId: currentUser() }, console.log(addanno)),
				contentType: "application/json; charset=utf-8",
				success: function (result) {
					annotation(result);
					annotationExist(true);
					console.log(result);
					 
				},
				error: function (err) {
					alert(err.status + " - " + err.statusText);
				}
			});
		};

		function editAnnotationData() {
			$(".anno-modal.edit").modal('toggle');
			$.ajax({
				type: "PUT",
				url: "http://localhost:3133/api/annotations",
				data: ko.toJSON({ Body: annotation().Body, PostId:annotation().PostId, UserId: annotation().UserId, Date:annotation().Date }, console.log(addanno)),
				contentType: "application/json; charset=utf-8",
				success: function (result) {
				},
				error: function (err) {
					alert(err.status + " - " + err.statusText);
				}
			});
		}

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
			fave: fave,
			annotationExist: annotationExist,
			annotation: annotation,
			currentUser: currentUser,
			editAnnotationData:editAnnotationData
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

	function CommentItem(data) {
		var self = this;
		self.CreationDate = data.creationDate;
		self.Body = data.text;
		self.Owner = data.userId;
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

	function AnnotationItem(data) {
		var self = this;
		self.PostId = data.PostId;
		self.UserId = data.UserId;
		self.Date = data.CreationDate;
		self.Body = data.Body;
	}

	return viewModel;
});