(function (angular) {
    'use strict';

    var module = angular.module('seedApp.admin', ['seedApp', 'seedApp.templates', 'ngResource']);

    module.config(['$stateProvider', function ($stateProvider) {
        $stateProvider
            .state('app.admin', {
                url: '/admin',
                views: {
                    'content@app.admin': {
                        templateUrl: 'admin/home/Home.html'
                    },
                    '@': {
                        templateUrl: 'admin/Layout.html'
                    }
                },
                data: {
                    title: 'Administration',
                    requireRole: 'admin'
                }
            })

            .state('app.admin.lookups', {
                url: '/lookups',
                views: {
                    'content@app.admin': {
                        templateUrl: 'admin/lookups/Index.html'
                    }
                },
                data: {
                    title: 'Manage Reference Data'
                }
            })

            .state('app.admin.lookups.list', {
                url: '/:type',
                views: {
                    'content@app.admin': {
                        templateUrl: 'admin/lookups/LookupList.html',
                        controller: 'LookupListController'
                    }
                },
                resolve: {
                    items: ['$stateParams', 'LookupsApi', function ($stateParams, LookupsApi) {
                        return LookupsApi.query({ type: $stateParams.type, pageNumber: 1, pageSize: 10 }).$promise;
                    }],
                    model: ['$stateParams', function ($stateParams) {
                        return {
                            typeName: $stateParams.type
                        };
                    }]
                },
                data: {
                    title: 'Manage {{ model.typeName }}'
                }
            })

            .state('app.admin.lookups.list.create', {
                url: '/create',
                views: {
                    'content@app.admin': {
                        templateUrl: 'admin/lookups/LookupCreate.html',
                        controller: 'LookupCreateController'
                    }
                },
                data: {
                    title: 'Create {{ model.typeName }}'
                }
            })

            .state('app.admin.lookups.list.edit', {
                url: '/:id/edit',
                views: {
                    'content@app.admin': {
                        templateUrl: 'admin/lookups/LookupEdit.html',
                        controller: 'LookupEditController'
                    }
                },
                resolve: {
                    model: ['$stateParams', 'LookupsApi', function ($stateParams, LookupsApi) {
                        return LookupsApi.get({ type: $stateParams.type, id: $stateParams.id }).$promise;
                    }]
                },
                data: {
                    title: 'Edit {{ model.typeName }} {{ model.name }}.'
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
                        return UsersApi.query({ pageNumber: 1, pageSize: 10 }).$promise;
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