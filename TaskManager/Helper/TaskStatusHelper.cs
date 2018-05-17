using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Models;
using TaskManager.Repository;

namespace TaskManager.Helper
{
    public class TaskStatusHelper
    {

        private readonly TaskRepository TaskRepo; 

        public TaskStatusHelper(TaskRepository taskRepo)
        {
            this.TaskRepo = taskRepo; 
        }
        public String validateStatusChange(Task task)
        {
            var taskOld = TaskRepo.GetById(task.Id);
            //Se não mudou nada, então tudo certo.
            if (taskOld == null || taskOld.Status.Equals(task.Status))
                return "";

            switch (task.Status)
            {
                case (TaskStatus.PENDING):
                    return "A tarefa não pode voltar a ficar PENDENTE";
                case (TaskStatus.PRODUCTION):
                    var error = verifyProductionStatus(task, taskOld);
                    if ( error != null && !error.Equals(""))
                        return error;
                    break;
                case (TaskStatus.SUSPENSION):
                    if(taskOld.Status != TaskStatus.PRODUCTION)
                        return "A tarefa não está no seguinte status: PRODUÇÃO para entrar em SUSPENSA";
                    break;
                case (TaskStatus.FINISHED):
                    if (taskOld.Status != TaskStatus.PRODUCTION)
                        return "A tarefa não está no seguinte status: PRODUÇÃO para entrar em SUSPENSA";
                    break;
                default:
                    return "";
                    
            }
            return "";

        }
        private string verifyProductionStatus(Task task, Task taskOld)
        {
            Task taskBlock = TaskRepo.GetTaskByUserAndStatus(task.Sponsor.Id, TaskStatus.PRODUCTION);
            if(taskBlock != null)
            {
                return "Este usuário já possui uma tarefa em produção.";
            }
            var subTasksPending = TaskRepo.GetSubtasksNotFinished(task.Id);
            if (subTasksPending != null && subTasksPending.Count() > 0)
            {
                return "Existem tarefas de pré-requisito não finalizada";
            }
            if (taskOld.Status != TaskStatus.PENDING && taskOld.Status != TaskStatus.SUSPENSION)
            {
                return "A tarefa não está nos seguintes status: PENDENTE ou SUSPENSA para ficar em PRODUÇÃO";
            }
            else
            {
                return "";
            }
        }
    }
}