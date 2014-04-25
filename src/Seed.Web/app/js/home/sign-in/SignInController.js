(function (angular) {
    'use strict';

    var module = angular.module('seedApp.home');

    module.controller('SignInController', ['$scope', '$state', 'AuthenticationApi', 'Principal',
        function ($scope, $state, AuthenticationApi, Principal) {
            $scope.model = {
                userName: '',
                password: '',
                error: ''
            };

            $scope.signIn = function () {
                $scope.model.error = '';

                if (!$scope.signin_form.$valid) {
                    return;
                }

                AuthenticationApi.signIn($scope.model.userName, $scope.model.password)
                    .success(function (data) {
                        Principal.signIn(data.userName, data.roles);

                        $state.go('home');
                    })
                    .error(function (data, status, headers, config) {
                        $scope.model.error = data.message;
                    });
            };
        }
    ]);
})(angular);