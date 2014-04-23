(function (angular) {
    'use strict';

    var module = angular.module('seedApp.dashboard', ['seedApp.templates', 'seedApp']);

    module.config(['$stateProvider', function ($stateProvider) {
        $stateProvider
            .state('dashboard', {
                url: '/dashboard',
                abstract: true,
                templateUrl: 'dashboard/Layout.html'
            })

            .state('dashboard.home', {
                url: '/',
                templateUrl: 'dashboard/Dashboard.html',
                controller: 'DashboardController',
                data: {
                    title: 'Dashboard'
                }
            })

            .state('dashboard.sign-out', {
                url: '/sign-out',
                controller: 'SignOutController',
                data: {
                    title: 'Sign out'
                }
            });
    }]);
})(angular);