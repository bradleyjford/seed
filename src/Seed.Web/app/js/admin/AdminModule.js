(function (angular) {
    'use strict';

    var module = angular.module('seedApp.admin', ['seedApp', 'seedApp.templates']);

    module.config(['$stateProvider', function ($stateProvider) {
        $stateProvider
            .state('admin', {
                url: '/admin',
                abstract: true,
                templateUrl: 'admin/Layout.html'
            })

            .state('admin.users', {
                url: '/users',
                templateUrl: 'admin/users/UserList.html',
                controller: 'UserListController',
                data: {
                    title: 'Manage Users'
                }
            });
    }]);
})(angular);