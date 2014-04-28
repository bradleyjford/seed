(function (angular) {
    'use strict';

    var module = angular.module('seedApp.home', ['seedApp.templates', 'seedApp']);

    module.config(['$stateProvider', function ($stateProvider) {
        $stateProvider
            .state('sign-in', {
                url: '/?returnUrl',
                controller: 'SignInController',
                templateUrl: 'home/sign-in/SignIn.html',
                data: {
                    title: 'Sign in'
                }
            })

            .state('home', {
                url: '/home',
                views: {
                    '@': {
                        templateUrl: 'home/Layout.html'
                    },
                    'content@home': {
                        controller: 'DashboardController',
                        templateUrl: 'home/dashboard/Dashboard.html'
                    }
                },
                data: {
                    title: 'Dashboard'
                }
            })

            .state('sign-out', {
                url: '/sign-out',
                controller: 'SignOutController',
                data: {
                    title: 'Sign out'
                }
            });
    }]);
})(angular);