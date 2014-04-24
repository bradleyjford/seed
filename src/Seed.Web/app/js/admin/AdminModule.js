(function (angular) {
    'use strict';

    var module = angular.module('seedApp.admin', ['seedApp', 'seedApp.templates']);

    module.config(['$stateProvider', function ($stateProvider) {
        $stateProvider
            .state('admin', {
                url: '/admin',
                views: {
                    'content@': {
                        templateUrl: 'admin/home/Home.html'
                    },
                    '@': {
                        templateUrl: 'admin/Layout.html'
                    }
                },
                data: {
                    title: 'Administration'
                }
            })

            .state('admin.users', {
                url: '/users',
                views: {
                    'content@': {
                        templateUrl: 'admin/users/UserList.html',
                        controller: 'UserListController'
                    }
                },
                data: {
                    title: 'Manage Users'
                }
            })

            .state('admin.users.edit', {
                url: '/:userId',
                views: {
                    'content@admin': {
                        templateUrl: 'admin/users/UserEdit.html',
                        controller: 'UserEditController'
                    }
                },
                data: {
                    title: 'Edit User'
                }
            });
    }]);
})(angular);