(function (angular) {
    angular.module('seedApp.security')

        // <li s-require-role="admin"><a href="/test">Admin</a></li>
        .directive('sAuthorize', ['$rootScope', function ($rootScope) {
            return {
                restrict: 'A',
                scope: false,
                link: function (scope, element, attrs) {
                    var originalDisplay = element.css('display');

                    $rootScope.$watchCollection('user.roles', function (roles) {
                        if (attrs.sAuthorize === '' && $rootScope.user.isAuthenticated) {
                            element.css('display', originalDisplay);
                        }
                        else if ($rootScope.user.isInRole(attrs.sAuthorize)) {
                            element.css('display', originalDisplay);
                        }
                        else {
                            element.css('display', 'none');
                        }
                    });
                }
            };
        }]);
})(angular);