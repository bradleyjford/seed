(function (angular) {
    'use strict';

    angular.module("seedApp.security")
        .controller('SignInCtrl', ['$scope', '$location', 'AuthenticationSvc', function ($scope, $location, AuthenticationSvc) {
            $scope.model = {
                userName: '',
                password: ''
            };

            $scope.signIn = function () {
                if ($scope.signin_form.$valid) {
                    var response = AuthenticationSvc.signIn($scope.model.userName, $scope.model.password);

                    response
                        .success(function(data) {
                            $location.path('/test');
                        })
                        .error(function (data, status, headers, config) {
                            alert(status);
                        });
                }
            };
        }]);
})(angular);