module.exports = function (grunt) {
    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-jshint');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-less');
    grunt.loadNpmTasks('grunt-env');
    grunt.loadNpmTasks('grunt-preprocess');
    grunt.loadNpmTasks('grunt-recess');
    grunt.loadNpmTasks('grunt-karma');
    grunt.loadNpmTasks('grunt-html2js');
    grunt.loadNpmTasks('grunt-concurrent');

    grunt.registerTask('default', ['env:dev', 'timestamp', 'clean', 'html2js', 'jshint', 'preprocess', 'concat', 'copy', 'less']);
    grunt.registerTask('release', ['env:release', 'clean', 'jshint', 'preprocess', 'html2js', 'concat', 'uglify', 'copy', 'less']);

    grunt.registerTask('rebuild', ['env:dev', 'timestamp', 'clean', 'html2js', 'jshint', 'preprocess', 'concat', 'copy', 'less']);


    grunt.registerTask('timestamp', function () {
        grunt.log.subhead(Date());
    });

    var karmaConfig = function (configFile, customOptions) {
        var options = { configFile: configFile, keepalive: true };
        var travisOptions = process.env.TRAVIS && { browsers: ['Firefox'], reporters: 'dots' };
        return grunt.util._.extend(options, customOptions, travisOptions);
    };

    grunt.initConfig({
        distDir: '../../dist',
        libDir: '../../lib',
        env: {
            options: {
            },
            dev: {
                NODE_ENV: 'dev'
            },
            release: {
                NODE_ENV: 'release'
            }
        },
        pkg: grunt.file.readJSON('package.json'),
        src: {
            app: ['app/js/app.js'],
            js: ['app/js/**/*.js', '!(app/js/app.js)', '!(app/js/*Module.js)'],
            modules: ['app/js/**/*Module.js'],
            assets: ['app/assets/**', '!(app/assets/**/*.less)'],
            tpl: { 
                app: ['app/js/**/*.html'], 
                uibootstrap: ['app/lib/ui-bootstrap/template/**/*.html'] 
            },
            appJs: ['<%= src.app %>', '<%= src.modules %>', '<%= src.js %>'],

            specs: ['test/**/*.spec.js'],
            scenarios: ['test/**/*.scenario.js']
        },
        clean: {
            options: {
                force: true
            },
            files: ['<%= distDir %>/*']
        },
        copy: {
            assets: {
                files: [
                    { dest: '<%= distDir %>/assets', src: '<%= src.assets %>', expand: true, cwd: 'app/assets/' }
                ]
            },
            spaHost: {
                files: [
                    { dest: '<%= distDir %>', src: '**', expand: true, cwd: '../../lib/Seed.Web.SpaHost' }
                ]
            },
            lib: {
                files: [
                    { dest: '<%= distDir %>/js', src: '**/*.js', expand: true, cwd: 'app/lib', flatten: true }
                ]
            }
        },
        karma: {
            unit: { options: karmaConfig('config/karma.conf.js') },
            e2e: { options: karmaConfig('config/karma-e2e.conf.js') },
            watch: { options: karmaConfig('config/karma.conf.js', { singleRun: false, autoWatch: true }) }
        },
        jshint: {
            files: ['Gruntfile.js', '<%= src.appJs %>'],
            options: {
                curly: true,
                eqeqeq: true,
                immed: true,
                latedef: true,
                newcap: true,
                noarg: true,
                sub: true,
                boss: true,
                eqnull: true,
                globals: {}
            }
        },
        html2js: {
            app: {
                options: {
                    base: 'app/js'
                },
                src: ['<%= src.tpl.app %>'],
                dest: '<%= distDir %>/SeedApp.html.js',
                module: 'seedApp.templates'
            },
            uiBootstrap: {
                options: {
                    base: 'app/lib/ui-bootstrap'
                },
                src: 'app/lib/ui-bootstrap/template/**/*.html',
                dest: '<%= distDir %>/js/ui-bootstrap.html.js',
                module: 'ui-bootstrap.templates'
            }
        },
        concat: {
            appJs: {
                src: ['<%= src.appJs %>'],
                dest: '<%= distDir %>/<%= pkg.name %>.js'
            },
            angular: {
                src: [
                    'app/lib/angular/angular.js',
                    'app/lib/angular/angular-resource.js',
                    'app/lib/angular/angular-route.js'
                ],
                dest: '<%= distDir %>/js/angular.js'
            }
        },

        uglify: {
            options: {
                banner: '/*! <%= pkg.name %> <%= grunt.template.today("yyyy-mm-dd") %> */\n',
                report: 'min',
                sourceMap: 'app/js/<%= pkg.name %>-<%= pkg.version %>.map.js',
                sourceMapRoot: '/',
                sourceMapPrefix: 1,
                sourceMappingURL: '/js/<%= pkg.name %>-<%= pkg.version %>.map.js'
            },
            dist: {
                src: ['<%= concat.appjs.dest %>'],
                dest: '<%= distDir %>/<%= pkg.name %>.min.js'
            },
            angular: {
                src: ['<%= concat.angular.dest %>'],
                dest: '<%= distDir %>/js/angular.min.js'
            },
            bootstrap: {
                src: ['app/lib/bootstrap/bootstrap.js'],
                dest: '<%= distDir %>/js/bootstrap.min.js'
            },
            jquery: {
                src: ['app/lib/jquery/jquery.js'],
                dest: '<%= distDir %>/js/jquery.min.js'
            },
            uiBootstrap: {
                src: ['app/lib/ui-bootstrap/ui-bootstrap.js'],
                dest: '<%= distDir %>/js/ui-bootstrap.min.js'
            }
        },
        less: {
            bootstrap: {
                files: {
                    '<%= distDir %>/assets/css/bootstrap.css': 'app/assets/less/bootstrap/bootstrap.less'
                }
            },
            app: {
                files: {

                }
            }
        },
        preprocess: {
            options: {

            },
            html: {
                src: 'app/index.html',
                dest: '<%= distDir %>/Views/Home/Index.cshtml'
            }
        },
        watch: {
            bootstrapLess: {
                files: ['app/assets/less/bootstrap/*.less'],
                tasks: ['less:bootstrap']
            },
            uiBootstrapTemplates: {
                files: ['app/lib/ui-bootstrap/template/**/*.html'],
                tasks: ['html2js:uibootstrap']
            },
            appLess: {
                files: ['app/assets/less/**.less', '!(app/assets/less/bootstrap/*.less)'],
                tasks: ['less:app']
            },
            appJs: {
                files: ['app/js/**.js'],
                tasks: ['jshint', 'concat:appJs']
            },
            index: {
                files: ['app/index.html'],
                tasks: ['preprocess:html']
            },
            templates: {
                files: ['app/js/**/*.html'],
                tasks: ['html2js:app']
            }
        },
        concurrent: {
            target: {
                tasks: ['rebuild', 'watch'],
                options: {
                  logConcurrentOutput: true
                }
            }
        }
    });
};