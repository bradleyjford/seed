(function (angular) {
    'use strict';

    var module = angular.module('seedApp.admin', ['seedApp', 'seedApp.templates', 'ngResource']);

    module.config(['$stateProvider', function ($stateProvider) {
        $stateProvider
            .state('app.admin', {
                url: '/admin',
                views: {
                    'content@app': {
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

            .state('app.admin.users', {
                url: '/users?p',
                views: {
                    'content@app.admin': {
                        templateUrl: 'admin/users/UserList.html',
                        controller: 'UserListController'
                    }
                },
                resolve: {
                    users: ['UsersApi', function (UsersApi) {
                        return UsersApi.query({ pageNumber: 5, pageSize: 20 }).$promise;
                    }]
                },
                data: {
                    title: 'Manage Users'
                }
            })

            .state('app.admin.users.edit', {
                url: '/:userId',
                views: {
                    'content@app.admin': {
                        templateUrl: 'admin/users/UserEdit.html',
                        controller: 'UserEditController'
                    }
                },
                resolve: {
                    model: ['UsersApi', '$stateParams', function (UsersApi, $stateParams) {
                        return UsersApi.get({ userId: $stateParams.userId }).$promise;
                    }]
                },
                data: {
                    title: '{{ model.fullName }}'
                }
            });
    }]);
})(angular);