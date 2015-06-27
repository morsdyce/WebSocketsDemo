/*global angular */

/**
 * The main TodoMVC app module
 *
 * @type {angular.Module}
 */
angular.module('todomvc', ['ngRoute', 'vxWamp'])
	.config(function ($routeProvider, $wampProvider) {
	    'use strict';

	    $wampProvider.init({
	        url: 'ws://127.0.0.1:9000/',
	        realm: 'realm1'
	    });

		var routeConfig = {
			controller: 'TodoCtrl',
			templateUrl: 'todomvc-index.html',
			resolve: {
				store: function (todoStorage) {
					// Get the correct module (API or localStorage).
					return todoStorage.then(function (module) {
						module.get(); // Fetch the todo records in the background.
						return module;
					});
				}
			}
		};

		$routeProvider
			.when('/', routeConfig)
			.when('/:status', routeConfig)
			.otherwise({
				redirectTo: '/'
			});
	})
    .run(function($wamp) {
        $wamp.open();
    });
