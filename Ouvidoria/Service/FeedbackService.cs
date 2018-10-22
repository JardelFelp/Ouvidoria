using Ouvidoria.Models;
using Ouvidoria.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ouvidoria.Service
{
    public class FeedbackService
    {
        public static void CadastraDepoimento(Feedback depoimento)
        {
            FeedbackRepository.CadastraDepoimento(depoimento);
        }

        public static IEnumerable<Feedback> RetornaFeedbacks(int id)
        {
            return FeedbackRepository.RetornaFeedbacks(id);
        }
    }
}