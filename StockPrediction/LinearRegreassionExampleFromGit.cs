//namespace Stocks
//{
//    public class LinearRegreassionExampleFromGit
//    {
//        // The multivariate linear regression is a generalization of
//        // the multiple linear regression. In the multivariate linear
//        // regression, not only the input variables are multivariate,
//        // but also are the output dependent variables.

//        // In the following example, we will perform a regression of
//        // a 2-dimensional output variable over a 3-dimensional input
//        // variable.

//        double[][] inputs =
//        {
//            // variables:  x1  x2  x3
//            new double[] {  1,  1,  1 }, // input sample 1
//            new double[] {  2,  1,  1 }, // input sample 2
//            new double[] {  3,  1,  1 }, // input sample 3
//        };

//        double[][] outputs =
//        {
//            // variables:  y1  y2
//            new double[] {  2,  3 }, // corresponding output to sample 1
//            new double[] {  4,  6 }, // corresponding output to sample 2
//            new double[] {  6,  9 }, // corresponding output to sample 3
//        };

//        // With a quick eye inspection, it is possible to see that
//        // the first output variable y1 is always the double of the
//        // first input variable. The second output variable y2 is
//        // always the triple of the first input variable. The other
//        // input variables are unused. Nevertheless, we will fit a
//        // multivariate regression model and confirm the validity
//        // of our impressions:

//        // Use Ordinary Least Squares to create the regression
//        OrdinaryLeastSquares ols = new OrdinaryLeastSquares();

//        // Now, compute the multivariate linear regression:
//        static MultivariateLinearRegression regression = ols.Learn(inputs, outputs);

//        // We can obtain predictions using
//        double[][] predictions = regression.Transform(inputs);

//        // The prediction error is
//        double error = new SquareLoss(outputs).Loss(predictions); // 0

//        // At this point, the regression error will be 0 (the fit was
//        // perfect). The regression coefficients for the first input
//        // and first output variables will be 2. The coefficient for
//        // the first input and second output variables will be 3. All
//        // others will be 0.
//        // 
//        // regression.Coefficients should be the matrix given by
//        // 
//        // double[,] coefficients = {
//        //                              { 2, 3 },
//        //                              { 0, 0 },
//        //                              { 0, 0 },
//        //                          };
//        // 

//        // We can also check the r-squared coefficients of determination:
//        double[] r2 = regression.CoefficientOfDetermination(inputs, outputs);

//    }
//}