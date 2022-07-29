using System.ComponentModel;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectApollo : MonoBehaviour {
	//页面一
	public InputField MaxSteeringAngleInp;
	public InputField WheelbaseInp;
	public InputField TransmissionRatioInp;
	public InputField LateralErrorWeightingInp;
	public InputField WeightingOfLateralErrorRateInp;
	public InputField CourseErrorWeightingInp;
	public InputField CourseErrorRateWeightingInp;
	public InputField InertialNavigationInstallationAngleInpX;
	public InputField InertialNavigationInstallationAngleInpY;
	public InputField InertialNavigationInstallationAngleInpZ;
	//页面三
	public InputField IMUToMainAntennaParametersInpX;
	public InputField IMUToMainAntennaParametersInpY;
	public InputField IMUToMainAntennaParametersInpZ;
	public InputField IMUToMainAntennaParametersInpA;
	public InputField IMUToMainAntennaParametersInpB;
	public InputField IMUToMainAntennaParametersInpC;
	public InputField IMUToAuxiliaryAntennaParametersInpX;
	public InputField IMUToAuxiliaryAntennaParametersInpY;
	public InputField IMUToAuxiliaryAntennaParametersInpZ;
	public InputField IMUToAuxiliaryAntennaParametersInpA;
	public InputField IMUToAuxiliaryAntennaParametersInpB;
	public InputField IMUToAuxiliaryAntennaParametersInpC;
	//页面二
	public InputField DistanceToRouteLineInp;
	public InputField NavigationLineSpacingLineInp;  //导航线条数
	public InputField PointsPerLineInp;
	public Dropdown IsThereTurningRadius;
	public Dropdown TurningRadiusKind;
	public Dropdown IsTheOrientationCalculated ;

	String path;

	// Use this for initialization
	void Start () {
		String str1 = System.IO.Directory.GetCurrentDirectory();
		String[] aryLine = null;
		aryLine = str1.Split('/');
		// for(int i = 0; i < aryLine.GetLength(0);i++){
		// 	Debug.Log(aryLine[i]);
		// }
		path = "/" + aryLine[1] + "/" + aryLine[2] + "/apollo/modules/";
		//Debug.Log(path);
	}
	// Update is called once per frame
	void Update () {
	}
	public String GetData(String Npath,String S1,int Pos){
		String canbusPath = path + Npath;
		StreamReader sr = new StreamReader(canbusPath);
		//String[] allLine = null;
		List<String> allLine = new List<String>();
		String[] aryLine = null;
		List<String> chosen = new List<String>();
		String line;
		//String readLine;
		String data = "";
		while((line = sr.ReadLine()) != null){
			allLine.Add(line);
		}
		//readLine = allLine[2];
		for(int i = 0;i < allLine.Count;i++){
			aryLine = allLine[i].Split(' ');
			for(int j = 0; j < aryLine.GetLength(0);j++){
				if(aryLine[j] != ""){
					chosen.Add(aryLine[j]);
					//Debug.Log(aryLine[i]);
				}
			}
		}
		for(int j = 0; j < chosen.Count;j++){
			if(chosen[j] == S1){
				data = chosen[j + Pos];
				break;
			}
		}
		return data;
	}
	public String DelSymbols(String Line){
		String[] aryLine = null;
		String chosen;
		aryLine = Line.Split('\\');
		chosen = aryLine[0];
		return chosen;
	}
	public void ShowSetting(){
		String MSAData = GetData("canbus/conf/canbus_conf.pb.txt","max_steer_angle:",1);//方向盘最大转角
		MaxSteeringAngleInp.text = MSAData;
		String WTData = GetData("control/conf/lincoln.pb.txt","wheelbase:",1);//轴距
		WheelbaseInp.text = WTData;
		String TRData = GetData("control/conf/lincoln.pb.txt","steer_transmission_ratio:",1);//传动比
		TransmissionRatioInp.text = TRData;
		String LEWData = GetData("control/conf/lincoln.pb.txt","matrix_q:",1);//传动比
		LateralErrorWeightingInp.text = LEWData;
		String WOLERData = GetData("control/conf/lincoln.pb.txt","matrix_q:",3);//传动比
		WeightingOfLateralErrorRateInp.text = WOLERData;
		String CEWData = GetData("control/conf/lincoln.pb.txt","matrix_q:",5);//传动比
		CourseErrorWeightingInp.text = CEWData;
		String CERWData = GetData("control/conf/lincoln.pb.txt","matrix_q:",7);//传动比
		CourseErrorRateWeightingInp.text = CERWData;
		String INIAXData = GetData("drivers/gnss/conf/gnss_conf_tractor.txt","angle",1);//惯导安装角度
		InertialNavigationInstallationAngleInpX.text = INIAXData;
		String INIAYData = GetData("drivers/gnss/conf/gnss_conf_tractor.txt","angle",2);//惯导安装角度
		InertialNavigationInstallationAngleInpY.text = INIAYData;
		String INIAZData = GetData("drivers/gnss/conf/gnss_conf_tractor.txt","angle",3);//惯导安装角度
		String nINIAZData = DelSymbols(INIAZData);
		InertialNavigationInstallationAngleInpZ.text = nINIAZData;

		String IMUTMXData = GetData("drivers/gnss/conf/gnss_conf_tractor.txt","imutoant",2);//IMU至主天线参数
		IMUToMainAntennaParametersInpX.text = IMUTMXData;
		String IMUTMYData = GetData("drivers/gnss/conf/gnss_conf_tractor.txt","imutoant",3);//IMU至主天线参数
		IMUToMainAntennaParametersInpY.text = IMUTMYData;
		String IMUTMZData = GetData("drivers/gnss/conf/gnss_conf_tractor.txt","imutoant",4);//IMU至主天线参数
		//String IMUTMZData = DelSymbols(IMUTMZData);
		IMUToMainAntennaParametersInpZ.text = IMUTMZData;
		String IMUTMAData = GetData("drivers/gnss/conf/gnss_conf_tractor.txt","imutoant",5);//IMU至主天线参数
		IMUToMainAntennaParametersInpA.text = IMUTMAData;
		String IMUTMBData = GetData("drivers/gnss/conf/gnss_conf_tractor.txt","imutoant",6);//IMU至主天线参数
		IMUToMainAntennaParametersInpB.text = IMUTMBData;
		String IMUTMCData = GetData("drivers/gnss/conf/gnss_conf_tractor.txt","imutoant",7);//IMU至主天线参数
		String nIMUTMCData = DelSymbols(IMUTMCData);
		IMUToMainAntennaParametersInpC.text = nIMUTMCData;

		String IMUTAXData = GetData("drivers/gnss/conf/gnss_conf_tractor.txt","imutoant2",2);//IMU至辅天线参数
		IMUToAuxiliaryAntennaParametersInpX.text = IMUTAXData;
		String IMUTAYData = GetData("drivers/gnss/conf/gnss_conf_tractor.txt","imutoant2",3);//IMU至辅天线参数
		IMUToAuxiliaryAntennaParametersInpY.text = IMUTAYData;
		String IMUTAZData = GetData("drivers/gnss/conf/gnss_conf_tractor.txt","imutoant2",4);//IMU至辅天线参数
		IMUToAuxiliaryAntennaParametersInpZ.text = IMUTAZData;
		String IMUTAAData = GetData("drivers/gnss/conf/gnss_conf_tractor.txt","imutoant2",5);//IMU至辅天线参数
		IMUToAuxiliaryAntennaParametersInpA.text = IMUTAAData;
		String IMUTABData = GetData("drivers/gnss/conf/gnss_conf_tractor.txt","imutoant2",6);//IMU至辅天线参数
		IMUToAuxiliaryAntennaParametersInpB.text = IMUTABData;
		String IMUTCAData = GetData("drivers/gnss/conf/gnss_conf_tractor.txt","imutoant2",7);//IMU至辅天线参数
		String nIMUTCAData = DelSymbols(IMUTCAData);
		IMUToAuxiliaryAntennaParametersInpC.text = nIMUTCAData;

		String DTRLData = GetData("tools/record_play/conf/manager_conf.yaml","lines_distance:",1);//导航线间距
		DistanceToRouteLineInp.text = DTRLData;
		String NLSLData = GetData("tools/record_play/conf/manager_conf.yaml","lines_num:",1);//导航线条数
		NavigationLineSpacingLineInp.text = NLSLData;
		String PPLData = GetData("tools/record_play/conf/path_conf.yaml","points_num:",1);//每条线上点数
		PointsPerLineInp.text = PPLData;

		//public Dropdown IsThereTurningRadius;
		//public Dropdown TurningRadiusKind;
		//public Dropdown IsTheOrientationCalculated ;
		String ITTRData = GetData("tools/record_play/conf/manager_conf.yaml","type:",1);//是否有转弯半径
		IsThereTurningRadius.value =  int.Parse(ITTRData) - 1;
		String TRKData = GetData("tools/record_play/conf/manager_conf.yaml","semicircle_type:",1);//转弯半径类型
		TurningRadiusKind.value =  int.Parse(TRKData) - 1;
		String ITOCData = GetData("tools/record_play/conf/path_conf.yaml","type:",1);//时候计算朝向
		IsTheOrientationCalculated.value =  int.Parse(ITOCData) - 1;
	}
	public void EditCarSetting(){  //第一个界面
		//对第一个文件的操作  canbus/conf/canbus_conf.pb.txt
		List<String> canbusAllLine = new List<String>();
		String[] canbusAryLine = null;
		StreamReader sr = new StreamReader(path + "canbus/conf/canbus_conf.pb.txt");
		String canbusLine;
		while((canbusLine = sr.ReadLine()) != null){
			canbusAllLine.Add(canbusLine);
		}
		for(int i = 0;i < canbusAllLine.Count;i++){
			canbusAryLine = canbusAllLine[i].Split(' ');
			for(int j = 0; j < canbusAryLine.GetLength(0);j++){
				if(canbusAryLine[j] == "max_steer_angle:"){
					canbusAllLine[i] = "  max_steer_angle: " + MaxSteeringAngleInp.text;
				}
			}
		}
		File.Delete(path + "canbus/conf/canbus_conf.pb.txt");
		FileStream canbusFs = new FileStream(path + "canbus/conf/canbus_conf.pb.txt", FileMode.Create, FileAccess.Write);
		StreamWriter canbusSw = new StreamWriter(canbusFs);
		for(int i = 0;i < canbusAllLine.Count;i++){
			canbusSw.WriteLine(canbusAllLine[i]);//开始写入值
		}
		canbusSw.Close();
		canbusFs.Close();

		//对第二个文件的操作  control/conf/lincoln.pb.txt
		List<String> controlAllLine = new List<String>();
		String[] controlAryLine = null;
		StreamReader controlSr = new StreamReader(path + "control/conf/lincoln.pb.txt");
		String controlLine;
		while((controlLine = controlSr.ReadLine()) != null){
			controlAllLine.Add(controlLine);
		}
		for(int i = 0;i < controlAllLine.Count;i++){
			controlAryLine = controlAllLine[i].Split(' ');
			for(int j = 0; j < controlAryLine.GetLength(0);j++){
				if(controlAryLine[j] == "wheelbase:"){
					controlAllLine[i] = "  wheelbase: " + WheelbaseInp.text;
				}
				if(controlAryLine[j] == "steer_transmission_ratio:"){//传动比
					controlAllLine[i] = "  steer_transmission_ratio: " + TransmissionRatioInp.text;
				}
				if(controlAryLine[j] == "steer_single_direction_max_degree:"){
					controlAllLine[i] = "  steer_single_direction_max_degree: " + MaxSteeringAngleInp.text;
				}
				if(controlAryLine[j] == "matrix_q:"){
					controlAllLine[i] = "  matrix_q: " + LateralErrorWeightingInp.text;
					controlAllLine[i+1] = "  matrix_q: " + WeightingOfLateralErrorRateInp.text;
					controlAllLine[i+2] = "  matrix_q: " + CourseErrorWeightingInp.text;
					controlAllLine[i+3] = "  matrix_q: " + CourseErrorRateWeightingInp.text;
					i = i + 3;
				}
			}
		}
		File.Delete(path + "control/conf/lincoln.pb.txt");
		FileStream controlFs = new FileStream(path + "control/conf/lincoln.pb.txt", FileMode.Create, FileAccess.Write);
		StreamWriter controlSw = new StreamWriter(controlFs);
		for(int i = 0;i < controlAllLine.Count;i++){
			controlSw.WriteLine(controlAllLine[i]);//开始写入值
		}
		controlSw.Close();
		controlFs.Close();


		//对第三个文件的操作  drivers/gnss/conf/gnss_conf_tractor.txt
		List<String> driversAllLine = new List<String>();
		String[] driversAryLine = null;
		StreamReader driverssr = new StreamReader(path + "drivers/gnss/conf/gnss_conf_tractor.txt");
		String driversLine;
		while((driversLine = driverssr.ReadLine()) != null){
			driversAllLine.Add(driversLine);
		}
		for(int i = 0;i < driversAllLine.Count;i++){
			driversAryLine = driversAllLine[i].Split(' ');
			for(int j = 0; j < driversAryLine.GetLength(0);j++){
				if(driversAryLine[j] == "angle"){
					driversAllLine[i] = "login_commands: \"config ins angle " + InertialNavigationInstallationAngleInpX.text + " " + 
					InertialNavigationInstallationAngleInpY.text + " " + InertialNavigationInstallationAngleInpZ.text + "\\r\\n\"";
					break;
				}
			}
		}
		File.Delete(path + "drivers/gnss/conf/gnss_conf_tractor.txt");
		FileStream driversFs = new FileStream(path + "drivers/gnss/conf/gnss_conf_tractor.txt", FileMode.Create, FileAccess.Write);
		StreamWriter driversSw = new StreamWriter(driversFs);
		for(int i = 0;i < driversAllLine.Count;i++){
			driversSw.WriteLine(driversAllLine[i]);//开始写入值
		}
		driversSw.Close();
		driversFs.Close();


		//对第四个文件的操作  tools/record_play/rtk_recorder.py
		List<String> toolsAllLine = new List<String>();
		String[] toolsAryLine = null;
		StreamReader toolssr = new StreamReader(path + "tools/record_play/rtk_recorder.py");
		String toolsLine;
		while((toolsLine = toolssr.ReadLine()) != null){
			toolsAllLine.Add(toolsLine);
		}
		for(int i = 0;i < toolsAllLine.Count;i++){
			toolsAryLine = toolsAllLine[i].Split(' ');
			for(int j = 0; j < toolsAryLine.GetLength(0);j++){
				if(toolsAryLine[j] == "math.tan(math.radians(carsteer"){
					toolsAllLine[i] = "        curvature = math.tan(math.radians(carsteer / 100 * " + MaxSteeringAngleInp.text + ") / " +
					MaxSteeringAngleInp.text + ") / 2.9";
				}
			}
		}
		File.Delete(path + "tools/record_play/rtk_recorder.py");
		FileStream toolsFs = new FileStream(path + "tools/record_play/rtk_recorder.py", FileMode.Create, FileAccess.Write);
		StreamWriter toolsSw = new StreamWriter(toolsFs);
		for(int i = 0;i < toolsAllLine.Count;i++){
			toolsSw.WriteLine(toolsAllLine[i]);//开始写入值
		}
		toolsSw.Close();
		toolsFs.Close();
	}


	public void EditNumberSetting(){  //第二个界面
		// public InputField DistanceToRouteLineInp;
		// public InputField NavigationLineSpacingLineInp;  //导航线条数
		// public InputField PointsPerLineInp;
		// public Dropdown IsThereTurningRadius;
		// public Dropdown TurningRadiusKind;
		// public Dropdown IsTheOrientationCalculated;
		//对第一个文件的操作  tools/record_play/conf/manager_conf.yaml
		List<String> toolsAllLine = new List<String>();
		String[] toolsAryLine = null;
		StreamReader toolssr = new StreamReader(path + "tools/record_play/conf/manager_conf.yaml");
		String toolsLine;
		while((toolsLine = toolssr.ReadLine()) != null){
			toolsAllLine.Add(toolsLine);
		}
		for(int i = 0;i < toolsAllLine.Count;i++){
			toolsAryLine = toolsAllLine[i].Split(' ');
			for(int j = 0; j < toolsAryLine.GetLength(0);j++){
				if(toolsAryLine[j] == "type:"){
					toolsAllLine[i] = "type: " + (IsThereTurningRadius.value + 1).ToString();
				}
				if(toolsAryLine[j] == "semicircle_type:"){
					toolsAllLine[i] = "semicircle_type: " + (TurningRadiusKind.value + 1).ToString();
				}
				if(toolsAryLine[j] == "lines_distance:"){
					toolsAllLine[i] = "lines_distance: " + DistanceToRouteLineInp.text;
				}
				if(toolsAryLine[j] == "lines_num:"){
					toolsAllLine[i] = "lines_num: " + NavigationLineSpacingLineInp.text;
				}
			}
		}
		File.Delete(path + "tools/record_play/conf/manager_conf.yaml");
		FileStream toolsFs = new FileStream(path + "tools/record_play/conf/manager_conf.yaml", FileMode.Create, FileAccess.Write);
		StreamWriter toolsSw = new StreamWriter(toolsFs);
		for(int i = 0;i < toolsAllLine.Count;i++){
			toolsSw.WriteLine(toolsAllLine[i]);//开始写入值
		}
		toolsSw.Close();
		toolsFs.Close();


		// public InputField DistanceToRouteLineInp;
		// public InputField NavigationLineSpacingLineInp;  //导航线条数
		// public InputField PointsPerLineInp;
		// public Dropdown IsThereTurningRadius;
		// public Dropdown TurningRadiusKind;
		// public Dropdown IsTheOrientationCalculated;
		//对第二个文件的操作  tools/record_play/conf/path_conf.yaml
		List<String> toolspAllLine = new List<String>();
		String[] toolspAryLine = null;
		StreamReader toolspsr = new StreamReader(path + "tools/record_play/conf/path_conf.yaml");
		String toolspLine;
		while((toolspLine = toolspsr.ReadLine()) != null){
			toolspAllLine.Add(toolspLine);
		}
		for(int i = 0;i < toolspAllLine.Count;i++){
			toolspAryLine = toolspAllLine[i].Split(' ');
			for(int j = 0; j < toolspAryLine.GetLength(0);j++){
				if(toolspAryLine[j] == "type:"){
					toolspAllLine[i] = "type: " + (IsTheOrientationCalculated.value + 1).ToString();
				}
				if(toolspAryLine[j] == "points_num:"){
					toolspAllLine[i] = "points_num: " + PointsPerLineInp.text;
				}
			}
		}
		File.Delete(path + "tools/record_play/conf/path_conf.yaml");
		FileStream toolspFs = new FileStream(path + "tools/record_play/conf/path_conf.yaml", FileMode.Create, FileAccess.Write);
		StreamWriter toolspSw = new StreamWriter(toolspFs);
		for(int i = 0;i < toolspAllLine.Count;i++){
			toolspSw.WriteLine(toolspAllLine[i]);//开始写入值
		}
		toolspSw.Close();
		toolspFs.Close();
	}



	public void EditIMUSetting(){  //第三个界面
		//对第三个文件的操作  drivers/gnss/conf/gnss_conf_tractor.txt
		List<String> driversAllLine = new List<String>();
		String[] driversAryLine = null;
		//List<String> aryLine = new List<String>();
		StreamReader driversSr = new StreamReader(path + "drivers/gnss/conf/gnss_conf_tractor.txt");
		String driversLine;
		while((driversLine = driversSr.ReadLine()) != null){
			driversAllLine.Add(driversLine);
		}
		for(int i = 0;i < driversAllLine.Count;i++){
			driversAryLine = driversAllLine[i].Split(' ');
			for(int j = 0; j < driversAryLine.GetLength(0);j++){
				if(driversAryLine[j] == "imutoant"){
					driversAllLine[i] = "login_commands: \"config imutoant offset " + IMUToMainAntennaParametersInpX.text + " " + 
					IMUToMainAntennaParametersInpY.text + " " + IMUToMainAntennaParametersInpZ.text + " " +
					IMUToMainAntennaParametersInpA.text + " " + IMUToMainAntennaParametersInpB.text + " " +
					IMUToMainAntennaParametersInpC.text + "\\r\\n\"";
				}
				if(driversAryLine[j] == "imutoant2"){
					driversAllLine[i] = "login_commands: \"config imutoant2 offset " + IMUToAuxiliaryAntennaParametersInpX.text + " " + 
					IMUToAuxiliaryAntennaParametersInpY.text + " " + IMUToAuxiliaryAntennaParametersInpZ.text + " " +
					IMUToAuxiliaryAntennaParametersInpA.text + " " + IMUToAuxiliaryAntennaParametersInpB.text + " " +
					IMUToAuxiliaryAntennaParametersInpC.text + "\\r\\n\"";
				}
			}
		}
		File.Delete(path + "drivers/gnss/conf/gnss_conf_tractor.txt");
		FileStream driversFs = new FileStream(path + "drivers/gnss/conf/gnss_conf_tractor.txt", FileMode.Create, FileAccess.Write);
		StreamWriter driversSw = new StreamWriter(driversFs);
		for(int i = 0;i < driversAllLine.Count;i++){
			driversSw.WriteLine(driversAllLine[i]);//开始写入值
		}
		driversSw.Close();
		driversFs.Close();
	}
}
