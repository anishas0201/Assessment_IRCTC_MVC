using Assessment_IRCTC_Revervation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment_IRCTC_Revervation.Interface
{
     public interface MainBodyInterface
    {
        ResponseModel SaveTrainDetails(TrainMaster roleuser);

        List<TrainMaster> GetTrainList();

        ResponseModel DeleteTrain(int Id);

        TrainMaster GetTrainbyId(int Id);

        ResponseModel UpdateTrainDetails(TrainMaster tmaster);


        ResponseModel SaveTicket(TicketBookingUser form);

        List<TicketBookingUser> GetTicketList();


    }
}
