(function (angular) {
    'use strict';

    var module = angular.module('seedApp.admin', ['seedApp', 'seedApp.templates', 'ngResource']);

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
                    'content@admin': {
                        templateUrl: 'admin/users/UserList.html',
                        controller: 'UserListController'
                    }
                },
                resolve: {
                    users: ['UsersApi', function (UsersApi) {
                        return UsersApi.query().$promise;
                    }]
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
                resolve: {
                    user: ['UsersApi', '$stateParams', function (UsersApi, $stateParams) {
                        return UsersApi.get({ userId: $stateParams.userId }).$promise;
                    }]
                },
                data: {
                    title: 'Edit User'
                }
            });
    }]);
})(angular);