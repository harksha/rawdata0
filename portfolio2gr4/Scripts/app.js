var my = my || {};

require(['knockout'], function (ko) {

	ko.components.register('Questions', {
		viewModel: { require: 'App/questionsViewModel' },
		template: { require: 'Scripts/text!AppContent/questions.html' }
	});

	ko.components.register('Users', {
		viewModel: { require: 'App/usersViewModel' },
		template: { require: 'Scripts/text!AppContent/users.html' }
	});

	ko.components.register('History', {
		viewModel: { require: 'App/historyViewModel' },
		template: { require: 'Scripts/text!AppContent/history.html' }
	});

	ko.components.register('Annotations', {
		viewModel: { require: 'App/annotationsViewModel' },
		template: { require: 'Scripts/text!AppContent/annotations.html' }
	});
	ko.components.register('app', {
		viewModel: { require: 'App/mainViewModel'},
		template: { require: 'Scripts/text!main.html' }
	});

	ko.applyBindings();
});
