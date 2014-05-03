(function (angular) {
    'use strict';

    var module = angular.module('seedApp');

    module.directive('saAuthorize', [function () {
        return {
            restrict: 'A',
            scope: false,
            link: function (scope, element, attrs) {
                var originalDisplay = element.css('display');

                scope.$on('securityPrincipalSignedIn', function (event, roles) {
                    if (attrs.saAuthorize === '' ||
                        roles.indexOf(attrs.saAuthorize) !== -1) {

                        element.css('display', originalDisplay);
                    }
                    else {
                        element.css('display', 'none');
                    }
                });

                scope.$on('securityPrincipalSignedOut', function (event) {
                    element.css('display', 'none');
                });
            }
        };
    }]);
})(angular);