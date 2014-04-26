(function (angular) {
    'use strict';

    var module = angular.module('seedApp.home');

    module.controller('SignInController', ['$scope', '$state', 'AuthenticationApi', 'Principal',
        function ($scope, $state, AuthenticationApi, Principal) {
            $scope.model = {
                username: '',
                password: '',
                error: ''
            };

            $scope.signIn = function () {
                $scope.model.error = '';

                if (!$scope.signInForm.$valid) {
                    return;
                }

                AuthenticationApi.signIn($scope.model.username, $scope.model.password)
                    .success(function (data) {
                        Principal.signIn(data.username, data.fullName, data.roles);

                        $state.go('home');
                    })
                    .error(function (data, status, headers, config) {
                        $scope.model.error = data.message;
                    });
            };
        }
    ]);
})(angular);