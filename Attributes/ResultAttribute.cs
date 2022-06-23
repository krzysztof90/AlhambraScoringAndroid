using AlhambraScoringAndroid.GamePlay;
using System;

namespace AlhambraScoringAndroid.Attributes
{
    public class ResultAttribute : Attribute
    {
        public ResultType ResultType;

        public ResultAttribute(ResultType resultType)
        {
            ResultType = resultType;
        }
    }
}